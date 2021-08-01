using System;
using System.Collections.Generic;

namespace realworlddotnet.Domain.Entities
{
    public class Article
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public User Author { get; set; } = null!;

        public List<Comment> Comments { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}