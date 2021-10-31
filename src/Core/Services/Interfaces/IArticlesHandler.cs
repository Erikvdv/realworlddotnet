using System.Threading;
using System.Threading.Tasks;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Core.Services.Interfaces;

public interface IArticlesHandler
{
    public Task<Article> CreateArticleAsync(
        NewArticleDto newArticle, string username, CancellationToken cancellationToken);

    public Task<Article> UpdateArticleAsync(
        ArticleUpdateDto update, string slug, string username, CancellationToken cancellationToken);

    public Task DeleteArticleAsync(string slug, string username, CancellationToken cancellationToken);

    public Task<ArticlesResponseDto> GetArticlesAsync(
        ArticlesQuery query, CancellationToken cancellationToken);

    public Task<Article> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken);

    public Task<Article> AddFavoriteAsync(string slug, string username, CancellationToken cancellationToken);

    public Task<Article> DeleteFavorite(string slug, string username, CancellationToken cancellationToken);
}
