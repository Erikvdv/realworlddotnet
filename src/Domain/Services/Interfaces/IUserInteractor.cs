using System.Threading;
using System.Threading.Tasks;
using realworlddotnet.Domain.Dto;

namespace realworlddotnet.Domain.Services.Interfaces
{
    public interface IUserInteractor
    {
        public Task<UserDto> CreateAsync(NewUserDto newUser, CancellationToken cancellationToken);
        
        public Task<UserDto> UpdateAsync(string username, UpdatedUserDto updatedUser, CancellationToken cancellationToken);
        public Task<UserDto> LoginAsync(LoginUserDto login, CancellationToken cancellationToken);
        public Task<UserDto> GetAsync(string username, CancellationToken cancellationToken);
    }
}