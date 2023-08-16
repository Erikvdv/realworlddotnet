namespace Realworlddotnet.Api.Features.Users;

public record UserEnvelope<T>([Required] T User);
