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
using System;

namespace Realworlddotnet.Api.Models
{

    public record ArticleEnvelope<T>(T Article);

    public record CommentEnvelope<T>(T comment);

    public record CommentsEnvelope<T>(T comments);

    public record Comment(int Id,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt,
        string Body,
        Author Author);

    public record Author(string Username, string Image, string Bio, bool Following);

    public record ArticleResponse(
        string Slug,
        string Title,
        string Description,
        string Body,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt,
        IEnumerable<string> TagList,
        Author Author,
        bool Favorited,
        int FavoritesCount);

    public record ArticlesResponse(IEnumerable<ArticleResponse> Articles, int ArticlesCount);

}
