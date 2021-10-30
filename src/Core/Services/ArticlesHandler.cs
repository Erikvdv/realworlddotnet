using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Infrastructure.Utils;

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
                newArticle.Title.GenerateSlug(),
                newArticle.Title,
                newArticle.Description,
                newArticle.Body,
                DateTime.UtcNow,
                DateTime.UtcNow
       )
            {
                Author = user,
                Tags = tags.ToList(),
                Comments = new List<Comment>()
            }
            ;

        _repository.AddArticle(article);
        await _repository.SaveChangesAsync();
        return article;
    }

    public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, CancellationToken cancellationToken)
    {
        return _repository.GetArticles(query, cancellationToken);
    }
}
