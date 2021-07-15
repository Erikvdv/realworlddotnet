using System.Threading.Tasks;
using realworlddotnet.Domain.Dto;

namespace realworlddotnet.Domain.Services.Interfaces
{
    public interface IUserInteractor
    {
        public Task<UserDto> CreateAsync(NewUserDto newUser);
        public Task<UserDto> UpdateAsync(string username, UpdatedUserDto updatedUser);
        public Task<UserDto> LoginAsync(LoginUserDto login);
        public Task<UserDto> GetAsync(string username);
    }
}