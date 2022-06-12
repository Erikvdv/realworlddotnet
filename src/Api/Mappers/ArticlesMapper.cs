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

namespace Realworlddotnet.Api.Mappers
{

    public static class ArticlesMapper
    {
        public static ArticleResponse MapFromArticleEntity(Article article)
        {
            var tags = article.Tags.Select(tag => tag.Id);
            var author = article.Author;
            var result = new ArticleResponse(
                article.Slug,
                article.Title,
                article.Description,
                article.Body,
                article.CreatedAt,
                article.UpdatedAt,
                tags,
                new Author(
                    author.Username,
                    author.Image,
                    author.Bio,
                    author.Followers.Any()),
                article.Favorited,
                article.FavoritesCount);
            return result;
        }

        public static ArticlesResponse MapFromArticles(ArticlesResponseDto articlesResponseDto)
        {
            var articles = articlesResponseDto.Articles
                .Select(articleEntity => MapFromArticleEntity(articleEntity))
                .ToList();
            return new ArticlesResponse(articles, articlesResponseDto.ArticlesCount);
        }
    }

}
