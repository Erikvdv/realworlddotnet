using System.ComponentModel.DataAnnotations;

namespace realworlddotnet.Api.Models
{
    public record UserEnvelope<T>([Required] T User);
}