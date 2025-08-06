namespace API
{
    public  class CorsLoging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorsLoging> _logger;

        public CorsLoging(RequestDelegate next, ILogger<CorsLoging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var origin = context.Request.Headers["Origin"].ToString();
            var method = context.Request.Method;
            var path = context.Request.Path;

            if (!string.IsNullOrEmpty(origin))
            {
                _logger.LogInformation("CORS request: {Method} {Path} Origin: {Origin}", method, path, origin);
            }

            await _next(context);

            if (context.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
            {
                _logger.LogInformation("CORS response headers sent for Origin: {Origin}", origin);
            }
        }
    }
}
