namespace BlogProject.Data.Middleware
{
    public class AccessDeniedMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessDeniedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.User.IsInRole("Admin")) // Check if the user is in the "Admin" role
            {
                // Redirect to an access denied page or another appropriate route
                context.Response.Redirect("/");
                return;
            }

            await _next(context);
        }
    }
}
