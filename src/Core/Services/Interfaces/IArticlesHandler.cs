using System.Threading;
using System.Threading.Tasks;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Core.Services.Interfaces;

public interface IArticlesHandler
{
    public Task<Article> CreateArticleAsync(
        NewArticleDto newArticle, string username, CancellationToken cancellationToken);

    public Task<ArticlesResponseDto> GetArticlesAsync(
        ArticlesQuery query, CancellationToken cancellationToken);
}
