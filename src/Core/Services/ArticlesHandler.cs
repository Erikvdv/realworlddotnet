using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;
using Realworlddotnet.Core.Services.Interfaces;

namespace Realworlddotnet.Core.Services;

public class ArticlesHandler : IArticlesHandler
{
    private readonly IConduitRepository _repository;

    public ArticlesHandler(IConduitRepository repository)
    {
        _repository = repository;
    }

    public async Task<Article> CreateArticleAsync(
        NewArticleDto newArticle, string username, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByUsernameAsync(username, cancellationToken);
        var tags = await _repository.UpsertTags(newArticle.TagList, cancellationToken);
        await _repository.SaveChangesAsync();

        var article = new Article(Guid.Empty,
                newArticle.Title,
                newArticle.Description,
                newArticle.Body,
                DateTime.UtcNow,
                DateTime.UtcNow
            ) { Author = user, Tags = tags.ToList(), Comments = new List<Comment>() }
            ;

        _repository.AddArticle(article);
        await _repository.SaveChangesAsync();
        return article;
    }

    public async Task<Article> UpdateArticleAsync(
        ArticleUpdateDto update, string slug, string username, CancellationToken cancellationToken)
    {
        var article = await _repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "Article not found"
            });
        
        if (username != article.Author.Username)
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 403, Detail = $"{username} is not the author"
            });
        
        article.UpdateArticle(update);
        await _repository.SaveChangesAsync();
        return article;
    }

    public async Task DeleteArticleAsync(string slug, string username, CancellationToken cancellationToken)
    {
        var article = await _repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "Article not found"
            });
        
        if (username != article.Author.Username)
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 403, Detail = $"{username} is not the author"
            });
        
        _repository.DeleteArticle(article);
        await _repository.SaveChangesAsync();
    }

    public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, CancellationToken cancellationToken)
    {
        return _repository.GetArticles(query, cancellationToken);
    }

    public async Task<Article> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        var article = await _repository.GetArticleBySlugAsync(slug, true, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "Article nog found"
            });
        }

        return article;
    }
}
