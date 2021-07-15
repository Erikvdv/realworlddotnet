using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using realworlddotnet.Api.Models;
using realworlddotnet.Domain.Dto;

namespace realworlddotnet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        [HttpGet("{username}")]
        public async Task<ActionResult<ProfilesEnvelope<Profile>>> GetProfileAsync(string username)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpPost("{username}/follow")]
        public async Task<ActionResult<UserEnvelope<UserDto>>> FollowUserAsync(string username)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpDelete("{username}/follow")]
        public async Task<ActionResult<UserEnvelope<UserDto>>> UnfollowUserAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}