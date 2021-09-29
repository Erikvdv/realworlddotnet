using System;
using System.Collections.Generic;

namespace realworlddotnet.Api.Models
{
    public record ArticleEnvelope<T>(T Article);

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
        int FavoritesCount
    );

    public record ArticlesResponse(IEnumerable<ArticleResponse> Articles, int ArticlesCount);
}