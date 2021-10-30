using System;

namespace Realworlddotnet.Core.Entities;

public class Comment
{
    public Comment(int commentId, string body, int authorId, int articleId,
        DateTime createdAt, DateTime updatedAt)
    {
        CommentId = commentId;
        Body = body;
        AuthorId = authorId;
        ArticleId = articleId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int CommentId { get; set; }

    public string Body { get; set; }

    public User Author { get; set; } = null!;

    public int AuthorId { get; set; }

    public Article Article { get; set; } = null!;

    public int ArticleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
