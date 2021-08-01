using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using realworlddotnet.Domain.Entities;
using realworlddotnet.Domain.Services.Interfaces;
using realworlddotnet.Infrastructure.Contexts;

namespace realworlddotnet.Infrastructure.Services
{
    public class ConduitRepository : IConduitRepository
    {
        private readonly ConduitContext _context;

        public ConduitRepository(ConduitContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            if (await _context.Users.AnyAsync(x => x.Username == user.Username))
            {
                throw new ProblemDetailsException(new ValidationProblemDetails
                {
                    Status = 422,
                    Detail = "Cannot register user",
                    Errors = {new KeyValuePair<string, string[]>("Username", new[] {"Username not available"})}
                });
            }
            
            if (await _context.Users.AnyAsync(x => x.Email == user.Email))
            {
                throw new ProblemDetailsException(new ValidationProblemDetails
                {
                    Status = 422,
                    Detail = "Cannot register user",
                    Errors = {new KeyValuePair<string, string[]>("Email", new[] {"Email address already in use"})}
                });
            }

            _context.Users.Add(user);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return  _context.Users.FirstAsync(x => x.Username == username);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}