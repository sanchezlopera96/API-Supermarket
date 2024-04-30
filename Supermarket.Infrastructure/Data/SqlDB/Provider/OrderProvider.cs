using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;
using Supermarket.Infrastructure.Data.SqlDB.Entities;
using Supermarket.Infrastructure.Data.SqlDB.UnitOfWork;
using Supermarket.Service.Interface;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Supermarket.Infrastructure.Data.SqlDB.Provider
{
    public class OrderProvider : IOrderProvider
    {
        public readonly IUnitOfWork _uowSupermarket;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public OrderProvider(IUnitOfWork uowSupermarket, IMapper mapper, IMemoryCache memoryCache)
        {
            _uowSupermarket = uowSupermarket;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }



        public async Task<OrderObjectResult> AddOrder(OrderObject orderObject)
        {
            try
            {
                object parameters = new
                {
                    @CodigoConsecutivo = 1
                };
                IEnumerable<OrderHeaderObjectEntity> lconsec = await _uowSupermarket.OrderHeaderObjectRepository.ExecuteStoreProcedureAsync<OrderHeaderObjectEntity>("SP_ObtenerMaximoPedido", parameters);
                OrderHeaderObjectEntity consec = lconsec.FirstOrDefault();
                var nextConsec = consec.IdPedido + 1;

                List<OrderDetailObjectEntity> orderDetails = new List<OrderDetailObjectEntity>();
                decimal montoTotalPedido = 0;

                foreach (var orderProduct in orderObject.ProductsOrder)
                {
                    IEnumerable<ProductObjectEntity> lproductsOrder = await _uowSupermarket.ProductObjectRepository.GetAsync(p => p.CodigoProducto == orderProduct.OrderProductCode);
                    ProductObjectEntity productOrder = lproductsOrder.FirstOrDefault();
                    if (productOrder.StockProducto >= orderProduct.OrderProductQuantity)
                    {
                        if (productOrder.EstadoProducto == true)
                        {
                            OrderDetailObjectEntity orderDetailEntity = new OrderDetailObjectEntity
                            {
                                IdPedido = nextConsec,
                                FechaPedido = DateTime.Now,
                                CodigoProductoPedido = orderProduct.OrderProductCode,
                                NombreProductoPedido = productOrder.NombreProducto,
                                CantidadProductoPedido = orderProduct.OrderProductQuantity,
                                PrecioUnitarioProductoPedido = productOrder.PrecioProducto,
                                PrecioTotalProductoPedido = (orderProduct.OrderProductQuantity * productOrder.PrecioProducto)
                            };

                            montoTotalPedido += orderDetailEntity.PrecioTotalProductoPedido;
                            orderDetails.Add(orderDetailEntity);
                            _uowSupermarket.OrderDetailObjectRepository.Insert(orderDetailEntity);
                            await _uowSupermarket.OrderDetailObjectRepository.CommitAsync();

                            productOrder.StockProducto = productOrder.StockProducto - orderProduct.OrderProductQuantity;
                            ProductObjectEntity update;
                            update = _mapper.Map<ProductObjectEntity>(productOrder);
                            _uowSupermarket.ProductObjectRepository.Update(update);
                            await _uowSupermarket.ProductObjectRepository.CommitAsync();
                        }
                        else
                        {
                            throw new Exception($"Inactive product");
                        }
                    }
                    else
                    {
                        throw new Exception($"Insufficient quantity of products");
                    }
                }

                OrderHeaderObjectEntity orderHeaderEntity = new OrderHeaderObjectEntity
                {
                    IdPedido = nextConsec,
                    FechaPedido = DateTime.Now,
                    IdentificacionCliente = orderObject.OrderIdentificationClient,
                    NombreCliente = orderObject.OrderNameClient,
                    CorreoCliente = orderObject.OrderCorreoClient,
                    MetodoPagoPedido = orderObject.OrderPaymentMehod,
                    MontoTotalPedido = montoTotalPedido,
                    DireccionPedido = orderObject.OrderAddress,
                    TransportistaPedido = orderObject.OrderConveyor,
                    DescripcionPedido = orderObject.OrderDescription,
                    EstadoPedido = orderObject.OrderStatus
                };
                
                _uowSupermarket.OrderHeaderObjectRepository.Insert(orderHeaderEntity);
                await _uowSupermarket.OrderHeaderObjectRepository.CommitAsync();

                object parametersResult = new
                {
                    @PedidoId = nextConsec
                };

                IEnumerable<OrderObjectResultEntity> lorder = await _uowSupermarket.ProductResultRepository.ExecuteStoreProcedureAsync<OrderObjectResultEntity>("SP_ObtenerPedidoPorId", parametersResult);
                OrderObjectResultEntity headerOrder = lorder.FirstOrDefault();
                List<ProductOrderResult> productsOrderResult = new List<ProductOrderResult>();
                List<OrderObjectResult> orderResult = new List<OrderObjectResult>();

                foreach (var orderProductResult in lorder)
                {
                    ProductOrderResult productOrderResult = new ProductOrderResult
                    {
                        OrderProductCode = orderProductResult.CodigoProductoPedido,
                        OrderProductName = orderProductResult.NombreProductoPedido,
                        OrderProductQuantity = orderProductResult.CantidadProductoPedido,
                        OrderProductPrice = orderProductResult.PrecioUnitarioProductoPedido,
                        OrderProductFullPrice = orderProductResult.PrecioTotalProductoPedido
                    };
                    productsOrderResult.Add(productOrderResult);
                }

                OrderObjectResult orderObjectResult = new OrderObjectResult
                {
                    OrderId = headerOrder.IdPedido,
                    OrderDate = headerOrder.FechaPedido,
                    OrderIdentificationClient = headerOrder.IdentificacionCliente,
                    OrderNameClient = headerOrder.NombreCliente,
                    OrderCorreoClient = headerOrder.CorreoCliente,
                    ProductsOrderResult = productsOrderResult.ToArray(),
                    OrderPaymentMehod = headerOrder.MetodoPagoPedido,
                    OrderPaymentMehodName = headerOrder.NombreMetodoPagoPedido,
                    OrderFullPayment = headerOrder.MontoTotalPedido,
                    OrderAddress = headerOrder.DireccionPedido,
                    OrderConveyor = headerOrder.TransportistaPedido,
                    OrderDescription = headerOrder.DescripcionPedido,
                    OrderStatus = headerOrder.EstadoPedido
                };
                orderResult.Add(orderObjectResult);
                OrderObjectResult resultOrder = _mapper.Map<OrderObjectResult>(orderResult.FirstOrDefault());
                SendOrderInformationEmail(resultOrder.OrderCorreoClient, resultOrder);
                return resultOrder;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating product data: {ex.Message}");
            }

        }

        public void SendOrderInformationEmail(string recipient, OrderObjectResult orderResult)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            string senderEmail = configuration["EmailSettings:SenderEmail"];
            string password = configuration["EmailSettings:Password"];

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage(senderEmail, recipient)
            {
                Subject = "Información del pedido # " + orderResult.OrderId + " de Supermarket",
                Body = GetOrderInformation(orderResult)
            };

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

        private string GetOrderInformation(OrderObjectResult orderResult)
        {
            string orderInfo = $"ID del pedido: {orderResult.OrderId}\n";
            orderInfo += $"Fecha del pedido: {orderResult.OrderDate.ToString("dd/MM/yyyy")}\n";
            orderInfo += $"Cliente: {orderResult.OrderNameClient}\n";
            orderInfo += $"Identificación: {orderResult.OrderIdentificationClient}\n";

            orderInfo += "Productos:\n";
            foreach (var product in orderResult.ProductsOrderResult)
            {
                orderInfo += $"- {product.OrderProductName}: {product.OrderProductQuantity} x {product.OrderProductPrice}\n";
            }

            orderInfo += $"Método de pago: {orderResult.OrderPaymentMehodName}\n";
            orderInfo += $"Total del pedido: {orderResult.OrderFullPayment}\n";
            orderInfo += $"Dirección de envío: {orderResult.OrderAddress}\n";
            orderInfo += $"Transportista: {orderResult.OrderConveyor}\n";
            orderInfo += $"Descripción del pedido: {orderResult.OrderDescription}\n";
            orderInfo += $"Estado del pedido: {orderResult.OrderStatus} En proceso\n";

            return orderInfo;
        }
    }
}