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
        await _repository.SaveChangesAsync(cancellationToken);

        var article = new Article(
                newArticle.Title,
                newArticle.Description,
                newArticle.Body
            ) { Author = user, Tags = tags.ToList() }
            ;

        _repository.AddArticle(article);
        await _repository.SaveChangesAsync(cancellationToken);
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
        await _repository.SaveChangesAsync(cancellationToken);
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
        await _repository.SaveChangesAsync(cancellationToken);
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
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }

        return article;
    }
    
    public async Task<Comment> AddCommentAsync(string slug, string username, CommentDto commentDto,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByUsernameAsync(username, cancellationToken);
        var article = await _repository.GetArticleBySlugAsync(slug, false, cancellationToken);
        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }
        
        var comment = new Comment(commentDto.body, user.Username, article.Id);
        _repository.AddArticleComment(comment);
        
        await _repository.SaveChangesAsync(cancellationToken);
        return comment;


    }

    public async Task<Article> AddFavoriteAsync(string slug, string username, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByUsernameAsync(username, cancellationToken);
        var article = await _repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }

        var articleFavorite = await _repository.GetArticleFavorite(user.Username, article.Id);

        if (articleFavorite is null)
        {
            _repository.AddArticleFavorite(new ArticleFavorite(user.Username, article.Id));
            await _repository.SaveChangesAsync(cancellationToken);
        }

        article = await _repository.GetArticleBySlugAsync(slug, false, cancellationToken);
        return article!;
    }

    public async Task<Article> DeleteFavorite(string slug, string username, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByUsernameAsync(username, cancellationToken);
        var article = await _repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }

        var articleFavorite = await _repository.GetArticleFavorite(user.Username, article.Id);

        if (articleFavorite is not null)
        {
            _repository.RemoveArticleFavorite(articleFavorite);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        article = await _repository.GetArticleBySlugAsync(slug, false, cancellationToken);
        return article!;
    }
}
