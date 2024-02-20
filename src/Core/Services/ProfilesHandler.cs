namespace Realworlddotnet.Core.Services;

public class ProfilesHandler(IConduitRepository repository) : IProfilesHandler
{
    public async Task<ProfileDto> GetAsync(string profileUsername, string? username,
        CancellationToken cancellationToken)
    {
        var profileUser = await repository.GetUserByUsernameAsync(profileUsername, cancellationToken);

        if (profileUser is null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Profile not found",
                Errors = { new KeyValuePair<string, string[]>("Profile", new[] { "not found" }) }
            });
        }

        var isFollowing = false;

        if (username is not null)
        {
            isFollowing = await repository.IsFollowingAsync(profileUsername, username, cancellationToken);
        }

        return new ProfileDto(profileUser.Username, profileUser.Bio, profileUser.Image, isFollowing);
    }

    public async Task<ProfileDto> FollowProfileAsync(string profileUsername, string username,
        CancellationToken cancellationToken)
    {
        var profileUser = await repository.GetUserByUsernameAsync(profileUsername, cancellationToken);

        if (profileUser is null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Profile not found",
                Errors = { new KeyValuePair<string, string[]>("Profile", new[] { "not found" }) }
            });
        }

        repository.Follow(profileUsername, username);
        await repository.SaveChangesAsync(cancellationToken);

        return new ProfileDto(profileUser.Username, profileUser.Bio, profileUser.Email, true);
    }

    public async Task<ProfileDto> UnFollowProfileAsync(string profileUsername, string username,
        CancellationToken cancellationToken)
    {
        var profileUser = await repository.GetUserByUsernameAsync(profileUsername, cancellationToken);

        if (profileUser is null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Profile not found",
                Errors = { new KeyValuePair<string, string[]>("Profile", new[] { "not found" }) }
            });
        }

        repository.UnFollow(profileUsername, username);
        await repository.SaveChangesAsync(cancellationToken);

        return new ProfileDto(profileUser.Username, profileUser.Bio, profileUser.Email, false);
    }
}
