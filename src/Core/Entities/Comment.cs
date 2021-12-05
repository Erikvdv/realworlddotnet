namespace Realworlddotnet.Core.Entities;

public class Comment
{
    public Comment(string body, string username, Guid articleId)
    {
        Body = body;
        Username = username;
        ArticleId = articleId;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; set; }
    public string Body { get; set; }
    public string Username { get; set; }
    public Guid ArticleId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User Author { get; set; } = null!;
    public Article Article { get; set; } = null!;
}
