using System;
using System.Collections.Generic;

namespace realworlddotnet.Domain.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public User Author { get; set; }

        public List<Comment> Comments { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}