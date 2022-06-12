﻿using System.Collections.Generic;
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

    public class User
    {
        public User()
        {
        }

        public User(NewUserDto newUser)
        {
            Password = newUser.Password;
            Username = newUser.Username;
            Email = newUser.Email;
        }

        public string Username { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string Bio { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public ICollection<ArticleFavorite>? ArticleFavorites { get; set; }
        public ICollection<Comment>? ArticleComments { get; set; }

        public ICollection<UserLink> Followers { get; set; } = new List<UserLink>();

        public ICollection<UserLink> FollowedUsers { get; set; } = new List<UserLink>();

        public void UpdateUser(UpdatedUserDto updatedUser)
        {
            Username = updatedUser.Username ?? Username;
            Email = updatedUser.Email ?? Email;
            Bio = updatedUser.Bio ?? Bio;
            Image = updatedUser.Image ?? Image;
            Password = updatedUser.Password ?? Password;
        }
    }

}
