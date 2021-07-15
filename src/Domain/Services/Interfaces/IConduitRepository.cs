using System.Threading.Tasks;
using realworlddotnet.Domain.Entities;

namespace realworlddotnet.Domain.Services.Interfaces
{
    public interface IConduitRepository
    {
        public Task AddUserAsync(User user);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User> GetUserByUsernameAsync(string username);
        public Task SaveChangesAsync();
    }
}