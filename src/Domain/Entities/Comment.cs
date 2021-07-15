using System;

namespace realworlddotnet.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Body { get; set; }

        public User Author { get; set; }

        public int AuthorId { get; set; }


        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}