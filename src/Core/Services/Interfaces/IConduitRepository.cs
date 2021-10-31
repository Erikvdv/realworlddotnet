using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Core.Services.Interfaces;

public interface IConduitRepository
{
    public Task AddUserAsync(User user);

    public Task<User?> GetUserByEmailAsync(string email);

    public Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);

    public Task<IEnumerable<Tag>> UpsertTags(IEnumerable<string> tags, CancellationToken cancellationToken);

    public Task SaveChangesAsync();

    public Task<ArticlesResponseDto> GetArticles(ArticlesQuery articlesQuery, CancellationToken cancellationToken);
    
    public Task<Article?> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken);

    public void AddArticle(Article article);
}
