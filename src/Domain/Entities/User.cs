using System;
using realworlddotnet.Domain.Dto;

namespace realworlddotnet.Domain.Entities
{
    public class User
    {
        public string Username { get; private set; }
        public string Email { get; private set;}
        public string Password { get; private set;}
        
        public string Bio { get; set; } = "";
        public string Image { get; set; } = "";
        
        public User()
        {
        }

        public User(NewUserDto newUser)
        {
            Password = newUser.Password;
            Username = newUser.Username;
            Email = newUser.Email;
        }

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