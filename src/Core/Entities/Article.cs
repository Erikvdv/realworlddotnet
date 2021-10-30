using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities;

public class Article
{
    public Article(Guid id, string slug, string title, string description, string body, 
        DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        Slug = slug;
        Title = title;
        Description = description;
        Body = body;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; set; }

    public string Slug { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Body { get; set; }

    public User Author { get; set; } = null!;

    public List<Comment> Comments { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<Tag> Tags { get; set; } = null!;
}
