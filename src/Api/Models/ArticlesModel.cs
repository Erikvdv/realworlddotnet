using System.ComponentModel.DataAnnotations;

namespace realworlddotnet.Api.Models
{
    
    public record ArticlesEnvelope<T>(T Articles);
    public record ArticleEnvelope<T>(T Article);
    

    public class Article
    {
        
    }

    public record ArticlesQuery(string? Tag, string? Author, string? Favorited, int? Limit, int? Offset);
    public record FeedQuery(int? Limit, int? Offset);
}