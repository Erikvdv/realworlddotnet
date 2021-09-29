using System.Threading;
using System.Threading.Tasks;
using realworlddotnet.Core.Dto;

namespace realworlddotnet.Core.Services.Interfaces
{
    public interface IUserHandler
    {
        public Task<UserDto> CreateAsync(NewUserDto newUser, CancellationToken cancellationToken);

        public Task<UserDto> UpdateAsync(string username, UpdatedUserDto updatedUser,
            CancellationToken cancellationToken);

        public Task<UserDto> LoginAsync(LoginUserDto login, CancellationToken cancellationToken);
        public Task<UserDto> GetAsync(string username, CancellationToken cancellationToken);
    }
}