namespace Supermarket.Infrastructure.Exceptions
{
    public class HttpNotFoundException : HttpRequestException
    {
        public HttpNotFoundException(string message) : base(message) { }
        public HttpNotFoundException(string message, System.Exception exception) :
         base(message, exception)
        { }
    }
}
