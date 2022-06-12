using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Realworlddotnet.Core.Entities;
using System;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Infrastructure.Utils;
using System.Threading;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Infrastructure.Utils.Interfaces;
using System.Linq;

namespace Realworlddotnet.Core.Services.Interfaces
{

    public interface IProfilesHandler
    {
        public Task<ProfileDto> GetAsync(string profileUsername, string? username, CancellationToken cancellationToken);

        public Task<ProfileDto> FollowProfileAsync(string profileUsername, string username,
            CancellationToken cancellationToken);

        public Task<ProfileDto> UnFollowProfileAsync(string profileUsername, string username,
            CancellationToken cancellationToken);
    }

}
