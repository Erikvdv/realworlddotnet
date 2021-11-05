using System;
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

    public Task SaveChangesAsync(CancellationToken cancellationToken);

    public Task<ArticlesResponseDto> GetArticles(ArticlesQuery articlesQuery, CancellationToken cancellationToken);
    
    public Task<Article?> GetArticleBySlugAsync(string slug, bool asNoTracking, CancellationToken cancellationToken);

    public void AddArticle(Article article);
    
    public void DeleteArticle(Article article);
    public void AddArticleComment(Comment comment);
    public Task<List<Comment>> GetCommentsBySlugAsync(string slug, CancellationToken cancellationToken);
    
    public Task<ArticleFavorite?> GetArticleFavorite(string username, Guid articleId);
    
    public void AddArticleFavorite(ArticleFavorite articleFavorite);
    
    public void RemoveArticleFavorite(ArticleFavorite articleFavorite);

    public Task<List<Tag>> GetTags(CancellationToken cancellationToken);

}
