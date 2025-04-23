using SolvexTest.Api.Authorization;

namespace SolvexTest.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token!);

            if (userId != null)
            {
                context.Items["User"] = userId;
            }

            await _next(context);
        }
    }
}
