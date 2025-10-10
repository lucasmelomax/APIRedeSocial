namespace RedeSocial.API.Middleware {
    public class ExceptionMiddleware {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro não tratado.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex) {
            var statusCode = ex switch {
                ArgumentException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status400BadRequest, 
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var result = System.Text.Json.JsonSerializer.Serialize(new {
                error = ex.Message,
                statusCode
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
