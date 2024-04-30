namespace Supermarket.Infrastructure.Exceptions
{
    public class HttpBadRequestException : HttpRequestException
    {
        public HttpBadRequestException(string message) : base(message) { }

        public HttpBadRequestException(string message, System.Exception exception) :
               base(message, exception)
        { }
    }
}