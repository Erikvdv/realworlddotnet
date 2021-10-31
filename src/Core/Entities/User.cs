using System.Collections.Generic;
using Realworlddotnet.Core.Dto;

namespace Realworlddotnet.Core.Entities;

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
    public ICollection<ArticleComment>? Comments { get; set; }

    public void UpdateUser(UpdatedUserDto updatedUser)
    {
        Username = updatedUser.Username ?? Username;
        Email = updatedUser.Email ?? Email;
        Bio = updatedUser.Bio ?? Bio;
        Image = updatedUser.Image ?? Image;
        Password = updatedUser.Password ?? Password;
    }
}
