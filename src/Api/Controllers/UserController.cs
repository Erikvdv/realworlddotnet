using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using realworlddotnet.Api.Models;
using realworlddotnet.Core.Dto;
using realworlddotnet.Core.Services.Interfaces;

namespace realworlddotnet.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserInteractor _userInteractor;

        public UserController(IUserInteractor userInteractor)
        {
            _userInteractor = userInteractor;
        }

        [HttpGet]
        public async Task<ActionResult<UserEnvelope<UserDto>>> GetUser(CancellationToken cancellationToken)
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userInteractor.GetAsync(username, cancellationToken);
            return Ok(new UserEnvelope<UserDto>(user));
        }

        [HttpPut]
        public async Task<ActionResult<UserEnvelope<UserDto>>> UpdateUser(
            RequestEnvelope<UserEnvelope<UpdatedUserDto>> request, CancellationToken cancellationToken)
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userInteractor.UpdateAsync(username, request.Body.User, cancellationToken);

            return Ok(new UserEnvelope<UserDto>(user));
        }
    }
}