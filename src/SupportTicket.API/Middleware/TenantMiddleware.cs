public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var tenantClaim = context.User.Claims
            .FirstOrDefault(c => c.Type == "tenantId");

        if (tenantClaim != null)
        {
            context.Items["TenantId"] = Guid.Parse(tenantClaim.Value);
        }

        await _next(context);
    }
}

