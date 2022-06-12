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
using System.Linq;

using CommentEntity = Realworlddotnet.Core.Entities.Comment;
using CommentModel = Realworlddotnet.Api.Models.Comment;

namespace Realworlddotnet.Api.Mappers
{

    public static class CommentMapper
    {
        public static CommentModel MapFromCommentEntity(CommentEntity commentEntity)
        {
            var author = new Author(
                commentEntity.Author.Username,
                commentEntity.Author.Image,
                commentEntity.Author.Bio,
                commentEntity.Author.Followers.Any());
            return new CommentModel(commentEntity.Id,
                commentEntity.CreatedAt,
                commentEntity.UpdatedAt,
                commentEntity.Body,
                author);
        }
    }

}
