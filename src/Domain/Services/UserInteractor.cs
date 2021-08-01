using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using realworlddotnet.Domain.Dto;
using realworlddotnet.Domain.Entities;
using realworlddotnet.Domain.Services.Interfaces;

namespace realworlddotnet.Domain.Services
{
    public class UserInteractor : IUserInteractor
    {
        private readonly IConduitRepository _repository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;

        public UserInteractor(IConduitRepository repository, ITokenGenerator tokenGenerator, IMapper mapper)
        {
            _repository = repository;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateAsync(NewUserDto newUser)
        {
            var user = new User(newUser);
            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();
            var token = _tokenGenerator.CreateToken(user.Username);
            return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
        }

        public async Task<UserDto> UpdateAsync(string username, UpdatedUserDto updatedUser)
        {
            var user = await _repository.GetUserByUsernameAsync(username);
            user.UpdateUser(updatedUser);
            await _repository.SaveChangesAsync();
            var token = _tokenGenerator.CreateToken(user.Username);
            return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
        }

        public async Task<UserDto> LoginAsync(LoginUserDto login)
        {
            var user = await _repository.GetUserByEmailAsync(login.Email);
            if (user == null || user.Password != login.Password)
            {
                throw new ProblemDetailsException(new ValidationProblemDetails
                {
                    Status = 422,
                    Detail = "Incorrect Credentials",
                    Errors = {new KeyValuePair<string, string[]>("Credentials", new[] {"incorrect credentials"})}
                });
            }

            var token = _tokenGenerator.CreateToken(user.Username);
            return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
        }

        public async Task<UserDto> GetAsync(string username)
        {
            var user = await _repository.GetUserByUsernameAsync(username);
            var token = _tokenGenerator.CreateToken(user.Username);
            return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
        }
    }
}