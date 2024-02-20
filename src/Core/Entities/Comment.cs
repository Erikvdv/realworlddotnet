namespace Realworlddotnet.Core.Entities;

public class Comment(string body, string username, Guid articleId)
{
    public int Id { get; set; }
    public string Body { get; set; } = body;
    public string Username { get; set; } = username;
    public Guid ArticleId { get; set; } = articleId;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public User Author { get; set; } = null!;
    public Article Article { get; set; } = null!;
}
