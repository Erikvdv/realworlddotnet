using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using realworlddotnet.Api.Models;
using realworlddotnet.Domain.Dto;
using realworlddotnet.Domain.Services.Interfaces;

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
        public async Task<ActionResult<UserEnvelope<UserDto>>> GetUser()
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userInteractor.GetAsync(username);
            var response = new UserEnvelope<UserDto>(user);
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<ActionResult<UserEnvelope<UserDto>>> UpdateUser(RequestEnvelope<UserEnvelope<UpdatedUserDto>> request)
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userInteractor.UpdateAsync(username, request.Body.User);

            var response = new UserEnvelope<UserDto>(user);
            return Ok(response);
        }
    }
}