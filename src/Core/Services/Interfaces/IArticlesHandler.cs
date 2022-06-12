﻿using System.Collections.Generic;
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

    public interface IArticlesHandler
    {
        public Task<Article> CreateArticleAsync(
            NewArticleDto newArticle, string username, CancellationToken cancellationToken);

        public Task<Article> UpdateArticleAsync(
            ArticleUpdateDto update, string slug, string username, CancellationToken cancellationToken);

        public Task DeleteArticleAsync(string slug, string username, CancellationToken cancellationToken);

        public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, string username, bool isFeed,
            CancellationToken cancellationToken);

        public Task<Article> GetArticleBySlugAsync(string slug, string username, CancellationToken cancellationToken);

        public Task<Comment> AddCommentAsync(string slug, string username, CommentDto commentDto,
            CancellationToken cancellationToken);

        public Task RemoveCommentAsync(string slug, int commentId, string username,
            CancellationToken cancellationToken);

        public Task<List<Comment>> GetCommentsAsync(string slug, string? username,
            CancellationToken cancellationToken);

        public Task<Article> AddFavoriteAsync(string slug, string username, CancellationToken cancellationToken);

        public Task<Article> DeleteFavorite(string slug, string username, CancellationToken cancellationToken);

        public Task<string[]> GetTags(CancellationToken cancellationToken);
    }

}
