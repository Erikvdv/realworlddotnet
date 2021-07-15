using System.ComponentModel.DataAnnotations;

namespace realworlddotnet.Api.Models
{
    public class ArticlesEnvelope<T>
    {
        [Required] 
        public T Articles { get; init; }
    }
    
    public class ArticleEnvelope<T>
    {
        [Required] 
        public T Article { get; init; }
    }

    public class Article
    {
        
    }

    public record ArticlesQuery(string? Tag, string? Author, string? Favorited, int? Limit, int? Offset);
    public record FeedQuery(int? Limit, int? Offset);
}