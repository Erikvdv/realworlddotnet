namespace Realworlddotnet.Api.Models;

public record UserEnvelope<T>([Required] T User);
