namespace Realworlddotnet.Core.Entities;

public class ArticleFavorite
{
    public ArticleFavorite(string username, Guid articleId)
    {
        Username = username;
        ArticleId = articleId;
    }

    public string Username { get; set; }

    public Guid ArticleId { get; set; }

    public User User { get; set; } = null!;

    public Article Article { get; set; } = null!;
}
