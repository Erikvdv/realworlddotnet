namespace Realworlddotnet.Core.Entities;

public class Article(string title, string description, string body)
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = title.GenerateSlug();
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public string Body { get; set; } = body;
    public User Author { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public bool Favorited { get; set; }
    public int FavoritesCount { get; set; } = 0;
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public List<Comment> Comments { get; set; } = new();
    public ICollection<ArticleFavorite> ArticleFavorites { get; set; } = new List<ArticleFavorite>();

    public void UpdateArticle(ArticleUpdateDto update)
    {
        if (!string.IsNullOrWhiteSpace(update.Title))
        {
            Title = update.Title;
            Slug = update.Title.GenerateSlug();
        }

        if (!string.IsNullOrWhiteSpace(update.Body))
        {
            Body = update.Body;
        }

        if (!string.IsNullOrWhiteSpace(update.Description))
        {
            Description = update.Description;
        }

        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
