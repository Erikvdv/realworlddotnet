using System.Threading;
using System.Threading.Tasks;
using realworlddotnet.Domain.Dto;
using realworlddotnet.Domain.Entities;

namespace realworlddotnet.Domain.Services.Interfaces
{
    public interface IArticlesInteractor
    {
        public Task<Article> CreateArticleAsync(NewArticleDto newArticle, string username, CancellationToken cancellationToken);
        public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, CancellationToken cancellationToken);
    }
}