using System.Net;

namespace NetKubernetes.Middleware
{
    public class MiddlewareException : Exception
    {
        public HttpStatusCode Codigo { get; set; }
        public object? Errors { get; set; }
        public MiddlewareException(HttpStatusCode codigo, object? errors = null)
        
        {
            Codigo = codigo;
            Errors = errors;
        }
    }
}