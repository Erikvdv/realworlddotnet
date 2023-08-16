namespace Realworlddotnet.Api.Features.Shared;

public record RequestEnvelope<T>
{
    [Required] [FromBody] public T Body { get; init; } = default!;
}
