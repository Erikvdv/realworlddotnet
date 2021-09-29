using System.Threading;
using System.Threading.Tasks;
using realworlddotnet.Core.Dto;
using realworlddotnet.Core.Entities;

namespace realworlddotnet.Core.Services.Interfaces
{
    public interface IArticlesInteractor
    {
        public Task<Article> CreateArticleAsync(NewArticleDto newArticle, string username, CancellationToken cancellationToken);
        public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, CancellationToken cancellationToken);
    }
}