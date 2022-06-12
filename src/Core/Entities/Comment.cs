using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Realworlddotnet.Core.Entities;
using System;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Infrastructure.Utils;
using System.Threading;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Infrastructure.Utils.Interfaces;
using System.Linq;

namespace Realworlddotnet.Core.Entities
{

    public class Comment
    {
        public Comment(string body, string username, Guid articleId)
        {
            Body = body;
            Username = username;
            ArticleId = articleId;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public int Id { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public Guid ArticleId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public User Author { get; set; } = null!;
        public Article Article { get; set; } = null!;
    }

}
