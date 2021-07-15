using System.ComponentModel.DataAnnotations;

namespace realworlddotnet.Api.Models
{
    public class ProfilesEnvelope<T>
    {
        [Required] public T Profile { get; init; }
    }

    public record Profile(string Username, string Bio, string Image, bool Following);
}