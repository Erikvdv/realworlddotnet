using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Api.Mappers;
using Realworlddotnet.Api.Models;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Realworlddotnet.Core.Services;
using Realworlddotnet.Data.Contexts;
using Realworlddotnet.Data.Services;
using Realworlddotnet.Infrastructure.Extensions.Authentication;
using Realworlddotnet.Infrastructure.Extensions.Logging;
using Realworlddotnet.Infrastructure.Extensions.ProblemDetails;
using Realworlddotnet.Infrastructure.Utils;
using Realworlddotnet.Infrastructure.Utils.Interfaces;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Realworlddotnet.Api.Controllers
{

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

}
