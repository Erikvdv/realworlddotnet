using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Realworlddotnet.Infrastructure.Extensions.Authentication;

public static class CustomOnMessageReceivedHandler
{
    public static Task OnMessageReceived(MessageReceivedContext context)
    {
        string authorization = context.Request.Headers["Authorization"];

        // If no authorization header found, nothing to process further
        if (string.IsNullOrEmpty(authorization))
        {
            context.NoResult();
            return Task.CompletedTask;
        }

        if (authorization.StartsWith("Token ", StringComparison.OrdinalIgnoreCase))
        {
            context.Token = authorization.Substring("Token ".Length).Trim();
        }

        // If no token found, no further work possible
        if (string.IsNullOrEmpty(context.Token))
        {
            context.NoResult();
        }

        return Task.CompletedTask;
    }
}
