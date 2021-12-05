namespace Realworlddotnet.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserHandler _userHandler;

    public UsersController(IUserHandler userHandler)
    {
        _userHandler = userHandler;
    }

    [HttpPost]
    public async Task<ActionResult<UserEnvelope<UserDto>>> Register(
        RequestEnvelope<UserEnvelope<NewUserDto>> request, CancellationToken cancellationToken)
    {
        var user = await _userHandler.CreateAsync(request.Body.User, cancellationToken);
        return Ok(new UserEnvelope<UserDto>(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserEnvelope<UserDto>>> Login(
        RequestEnvelope<UserEnvelope<LoginUserDto>> request, CancellationToken cancellationToken)
    {
        var user = await _userHandler.LoginAsync(request.Body.User, cancellationToken);
        return Ok(new UserEnvelope<UserDto>(user));
    }
}
