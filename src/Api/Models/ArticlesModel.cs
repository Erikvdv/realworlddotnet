namespace Realworlddotnet.Api.Models;

public record ArticleEnvelope<T>(T Article);

public record CommentEnvelope<T>(T comment);

public record CommentsEnvelope<T>(T comments);

public record Comment(int Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string Body,
    Author Author);

public record Author(string Username, string Image, string Bio, bool Following);

public record ArticleResponse(
    string Slug,
    string Title,
    string Description,
    string Body,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    IEnumerable<string> TagList,
    Author Author,
    bool Favorited,
    int FavoritesCount);

public record ArticlesResponse(IEnumerable<ArticleResponse> Articles, int ArticlesCount);
