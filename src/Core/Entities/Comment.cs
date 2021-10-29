using System;

namespace Realworlddotnet.Core.Entities;

public class Comment
{
    public Comment(int commentId, string body, User author, int authorId, Article article, int articleId,
        DateTime createdAt, DateTime updatedAt)
    {
        CommentId = commentId;
        Body = body;
        Author = author;
        AuthorId = authorId;
        Article = article;
        ArticleId = articleId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int CommentId { get; set; }

    public string Body { get; set; }

    public User Author { get; set; }

    public int AuthorId { get; set; }

    public Article Article { get; set; }

    public int ArticleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
