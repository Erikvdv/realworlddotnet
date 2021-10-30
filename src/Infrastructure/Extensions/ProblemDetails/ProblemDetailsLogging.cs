using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Realworlddotnet.Infrastructure.Extensions.ProblemDetails;

public class ProblemDetailsLogging : IPostConfigureOptions<ProblemDetailsOptions>
{
    private readonly ILogger<ProblemDetailsLogging> _logger;

    public ProblemDetailsLogging(ILogger<ProblemDetailsLogging> logger)
    {
        _logger = logger;
    }

    public void PostConfigure(string name, ProblemDetailsOptions options)
    {
        options.OnBeforeWriteDetails += (context, problem) => { _logger.LogInformation("{@problem}", problem); };
    }
}
