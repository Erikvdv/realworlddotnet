namespace Realworlddotnet.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
    private readonly IProfilesHandler _profilesHandler;

    public ProfilesController(IProfilesHandler profilesHandler)
    {
        _profilesHandler = profilesHandler;
    }

    private string Username => User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpGet("{username}")]
    public async Task<ActionResult<ProfilesEnvelope<ProfileDto>>> GetProfileAsync(string username,
        CancellationToken cancellationToken)
    {
        var result = await _profilesHandler.GetAsync(username, Username, cancellationToken);
        return new ProfilesEnvelope<ProfileDto>(result);
    }

    [Authorize]
    [HttpPost("{followUsername}/follow")]
    public async Task<ActionResult<ProfilesEnvelope<ProfileDto>>> FollowUserAsync(string followUsername,
        CancellationToken cancellationToken)
    {
        var result = await _profilesHandler.FollowProfileAsync(followUsername, Username, cancellationToken);
        return new ProfilesEnvelope<ProfileDto>(result);
    }

    [Authorize]
    [HttpDelete("{followUsername}/follow")]
    public async Task<ActionResult<ProfilesEnvelope<ProfileDto>>> UnfollowUserAsync(string followUsername,
        CancellationToken cancellationToken)
    {
        var result = await _profilesHandler.UnFollowProfileAsync(followUsername, Username, cancellationToken);
        return new ProfilesEnvelope<ProfileDto>(result);
    }
}
