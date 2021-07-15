using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using realworlddotnet.Api.Models;
using realworlddotnet.Domain.Dto;
using realworlddotnet.Domain.Services.Interfaces;

namespace realworlddotnet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserInteractor _userInteractor;

        public UsersController(ILogger<UsersController> logger, IUserInteractor userInteractor)
        {
            _logger = logger;
            _userInteractor = userInteractor;
        }

        [HttpPost]
        public async Task<ActionResult<UserEnvelope<UserDto>>> Register(RequestEnvelope<UserEnvelope<NewUserDto>> request)
        {
            var user = await _userInteractor.CreateAsync(request.Body.User);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserEnvelope<UserDto>>> Login(RequestEnvelope<UserEnvelope<LoginUserDto>> request)
        {
            var user = await _userInteractor.LoginAsync(request.Body.User);
            return Ok(user);
        }
    }
}