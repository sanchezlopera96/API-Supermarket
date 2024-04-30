namespace Supermarket.Infrastructure.Exceptions
{
    public class HttpTooManyRequestsException : HttpRequestException
    {
        public HttpTooManyRequestsException(string message) : base(message) { }
    }
}
