namespace LibraryManagement.Presentation.WebAPI.Middleware
{
    public class HeaderLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HeaderLoggingMiddleware> _logger;


        public HeaderLoggingMiddleware(RequestDelegate next, ILogger<HeaderLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            _logger.LogInformation("User-Agent: {UserAgent}", userAgent);

            var authHeader = context.Request.Headers["Authorization"].ToString();
            _logger.LogInformation("Authorization: {Auth}", string.IsNullOrEmpty(authHeader) ? "[none]" : authHeader);


            await _next(context);
        }
    }
}
