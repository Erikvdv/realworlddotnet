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

    public interface IUserHandler
    {
        public Task<UserDto> CreateAsync(NewUserDto newUser, CancellationToken cancellationToken);

        public Task<UserDto> UpdateAsync(
            string username, UpdatedUserDto updatedUser, CancellationToken cancellationToken);

        public Task<UserDto> LoginAsync(LoginUserDto login, CancellationToken cancellationToken);

        public Task<UserDto> GetAsync(string username, CancellationToken cancellationToken);
    }

}
