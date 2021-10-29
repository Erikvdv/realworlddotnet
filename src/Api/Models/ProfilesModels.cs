namespace Realworlddotnet.Api.Models;

public record ProfilesEnvelope<T>(T Profile);

public record Profile(string Username, string Bio, string Image, bool Following);
