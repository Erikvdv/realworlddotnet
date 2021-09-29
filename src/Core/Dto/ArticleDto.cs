using System.Collections.Generic;
using realworlddotnet.Core.Entities;

namespace realworlddotnet.Core.Dto
{
    public record NewArticleDto (string Title, string Description, string Body, IEnumerable<string> TagList);
    public record ArticlesResponseDto(List<Article> Articles, int ArticlesCount);
    public record ArticlesQuery(string? Tag, string? Author, string? Favorited, int Limit = 10, int Offset = 0);
    public record FeedQuery(int? Limit, int? Offset);
}