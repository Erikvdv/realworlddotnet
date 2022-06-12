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

    public class ArticleFavorite
    {
        public ArticleFavorite(string username, Guid articleId)
        {
            Username = username;
            ArticleId = articleId;
        }

        public string Username { get; set; }

        public Guid ArticleId { get; set; }

        public User User { get; set; } = null!;

        public Article Article { get; set; } = null!;
    }

}
