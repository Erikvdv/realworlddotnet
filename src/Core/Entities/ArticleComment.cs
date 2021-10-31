using System;

namespace Realworlddotnet.Core.Entities;

public class ArticleComment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public string Username { get; set; }
    public Guid ArticleId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public User Author { get; set; } = null!;
    public Article Article { get; set; } = null!;
    
    public ArticleComment(int id, string body, string username, Guid articleId)
    {
        Id = id;
        Body = body;
        Username = username;
        ArticleId = articleId;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
