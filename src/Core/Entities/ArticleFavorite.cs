using System;

namespace Realworlddotnet.Core.Entities;

public class ArticleFavorite
{
    public string Username { get;  set; }
    
    public Guid ArticleId { get; set; }

    public User User { get;  set; }
    
    public Article Article { get; set; }
    
    public ArticleFavorite(string username, Guid articleId)
    {
        Username = username;
        ArticleId = articleId;
    }
}
