namespace Realworlddotnet.Api.Controllers;

[Route("[controller]")]
[Authorize]
public class UserController(IUserHandler userHandler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<UserEnvelope<UserDto>>> GetUser(CancellationToken cancellationToken)
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await userHandler.GetAsync(username, cancellationToken);
        return Ok(new UserEnvelope<UserDto>(user));
    }

    [HttpPut]
    public async Task<ActionResult<UserEnvelope<UserDto>>> UpdateUser(
        RequestEnvelope<UserEnvelope<UpdatedUserDto>> request, CancellationToken cancellationToken)
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await userHandler.UpdateAsync(username, request.Body.User, cancellationToken);

        return Ok(new UserEnvelope<UserDto>(user));
    }
}
