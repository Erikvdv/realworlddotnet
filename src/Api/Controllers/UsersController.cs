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

}
