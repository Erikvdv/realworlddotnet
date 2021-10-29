using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Realworlddotnet.Api.Models;

public record RequestEnvelope<T>
{
    [Required] [FromBody] public T Body { get; init; } = default!;
}
