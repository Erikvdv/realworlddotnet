using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities;

public class Article
{
    public Article(Guid id, string slug, string title, string description, string body, User author,
        List<Comment> comments, DateTimeOffset createdAt, DateTimeOffset updatedAt, ICollection<Tag> tags)
    {
        Id = id;
        Slug = slug;
        Title = title;
        Description = description;
        Body = body;
        Author = author;
        Comments = comments;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Tags = tags;
    }

    public Guid Id { get; set; }

    public string Slug { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Body { get; set; }

    public User Author { get; set; }

    public List<Comment> Comments { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<Tag> Tags { get; set; }
}
