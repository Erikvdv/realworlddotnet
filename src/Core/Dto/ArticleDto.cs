using System.Collections.Generic;
using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Core.Dto;

public record NewArticleDto(string Title, string Description, string Body, IEnumerable<string> TagList);

public record ArticlesResponseDto(List<Article> Articles, int ArticlesCount);

public record ArticlesQuery(string? Tag, string? Author, string? Favorited, int Limit = 10, int Offset = 0);

public record FeedQuery(int? Limit, int? Offset);
