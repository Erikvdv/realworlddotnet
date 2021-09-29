using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using realworlddotnet.Core.Dto;
using realworlddotnet.Core.Entities;
using realworlddotnet.Core.Services.Interfaces;
using realworlddotnet.Infrastructure.Utils;

namespace realworlddotnet.Core.Services
{
    public class ArticlesInteractor : IArticlesInteractor
    {
        private readonly IConduitRepository _repository;

        public ArticlesInteractor(IConduitRepository repository)
        {
            _repository = repository;
        }

        public async Task<Article> CreateArticleAsync(NewArticleDto newArticle, string username, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetUserByUsernameAsync(username, cancellationToken);
            var tags = await _repository.UpsertTags(newArticle.TagList, cancellationToken);
            await _repository.SaveChangesAsync();
            
            var article = new Article()
            {
                Author = user,
                Body = newArticle.Body,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Description = newArticle.Description,
                Title = newArticle.Title,
                Slug = newArticle.Title.GenerateSlug(),
                Tags = tags.ToList()
            };
            
            _repository.AddArticle(article);
            await _repository.SaveChangesAsync();
            return article;
        }

        public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, CancellationToken cancellationToken)
        {
            return _repository.GetArticles(query, cancellationToken);
        }
    }
}