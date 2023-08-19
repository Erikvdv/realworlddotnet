using Realworlddotnet.Core.Dto;

namespace Realworlddotnet.Api.Features.Profiles;

public class ProfilesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("profiles")
            .RequireAuthorization()
            .WithTags("Profile")
            .IncludeInOpenApi();

        group.MapGet("{username}",
                async Task<Ok<ProfilesEnvelope<ProfileDto>>>
                    (string username, IProfilesHandler profilesHandler, ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result = await profilesHandler.GetAsync(username, user, new CancellationToken());
                    return TypedResults.Ok(new ProfilesEnvelope<ProfileDto>(result));
                })
            .WithName("GetProfile");

        group.MapPost("{followUsername}/follow",
                async Task<Ok<ProfilesEnvelope<ProfileDto>>>
                    (string followUsername, IProfilesHandler profilesHandler, ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result =
                        await profilesHandler.FollowProfileAsync(followUsername, user!, new CancellationToken());
                    return TypedResults.Ok(new ProfilesEnvelope<ProfileDto>(result));
                })
            .WithName("FollowProfile");

        group.MapDelete("{followUsername}/follow",
                async Task<Ok<ProfilesEnvelope<ProfileDto>>>
                    (string followUsername, IProfilesHandler profilesHandler, ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result =
                        await profilesHandler.UnFollowProfileAsync(followUsername, user!, new CancellationToken());
                    return TypedResults.Ok(new ProfilesEnvelope<ProfileDto>(result));
                })
            .WithName("UnfollowProfile");
    }
}
