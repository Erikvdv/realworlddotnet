using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Api.Models;
using Realworlddotnet.Core.Dto;

namespace Realworlddotnet.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
    [HttpGet("{username}")]
    public async Task<ActionResult<ProfilesEnvelope<Profile>>> GetProfileAsync(string username)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{username}/follow")]
    public async Task<ActionResult<UserEnvelope<UserDto>>> FollowUserAsync(string username)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpDelete("{username}/follow")]
    public async Task<ActionResult<UserEnvelope<UserDto>>> UnfollowUserAsync(string username)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
