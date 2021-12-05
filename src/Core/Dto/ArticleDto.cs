namespace Realworlddotnet.Core.Dto;

public record NewArticleDto(string Title, string Description, string Body, IEnumerable<string> TagList);

public record ArticleUpdateDto(string? Title, string? Description, string? Body) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Title) && string.IsNullOrWhiteSpace(Description) &&
            string.IsNullOrWhiteSpace(Body))
        {
            yield return new ValidationResult(
                $"At least on of the fields: {nameof(Title)}, {nameof(Description)}, {nameof(Body)} must be filled"
            );
        }
    }
}

public record ArticlesResponseDto(List<Article> Articles, int ArticlesCount);

public record ArticlesQuery(string? Tag, string? Author, string? Favorited, int Limit = 20, int Offset = 0);

public record FeedQuery(int Limit = 20, int Offset = 0);

public record CommentDto(string body);
