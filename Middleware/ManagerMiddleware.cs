using Newtonsoft.Json;

namespace NetKubernetes.Middleware
{
    public class ManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManagerMiddleware> _logger;

        public ManagerMiddleware(RequestDelegate next, ILogger<ManagerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManagerExceptionAsync(context, ex, _logger);
            }
        }

        private async Task ManagerExceptionAsync(HttpContext context, Exception ex, ILogger<ManagerMiddleware> logger)
        {
            object? errors = null;

            switch (ex)
            {
                case MiddlewareException me:
                    logger.LogError(ex, "Middleware error: {Message}", me.Message);
                    errors = me.Errors;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;

                case Exception e:
                    logger.LogError(ex, "Erro de Servidor");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Erro de Servidor" : e.Message;
                    context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                    break;
                default:
                    logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            var result = string.Empty;
            if (errors != null)
            {
                result =JsonConvert.SerializeObject(new { errors });
               
            }
                await context.Response.WriteAsync(result);
        }
    }
}
