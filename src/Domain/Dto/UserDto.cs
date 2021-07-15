using System.ComponentModel.DataAnnotations;

namespace realworlddotnet.Domain.Dto
{
    public class NewUserDto
    {
        [Required] public string Username { get; init; }
        [Required] public string Email { get; init; }
        [Required] public string Password { get; init; }
    }
    
    public class LoginUserDto
    {
        [Required] public string Email { get; init; }
        [Required] public string Password { get; init; }
    };
    
    /// <summary>
    /// At lease one of the items should be not null
    /// </summary>
    public class UpdatedUserDto
    {
        public string? Username { get; init; }
        public string? Email { get; init; }
        public string? Bio { get; init; }
        public string? Image { get; init; }
        public string? Password { get; init; }
    }

    public class UserDto
    {
        [Required] public string Username { get; init; }
        [Required] public string Email { get; init; }
        [Required] public string Token { get; set; }
        [Required] public string Bio { get; init; }
        [Required] public string Image { get; init; }
    }
}