namespace Realworlddotnet.Core.Services;

public class UserHandler : IUserHandler
{
    private readonly IConduitRepository _repository;
    private readonly ITokenGenerator _tokenGenerator;

    public UserHandler(IConduitRepository repository, ITokenGenerator tokenGenerator)
    {
        _repository = repository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<UserDto> CreateAsync(NewUserDto newUser, CancellationToken cancellationToken)
    {
        var user = new User(newUser);
        await _repository.AddUserAsync(user);
        await _repository.SaveChangesAsync(cancellationToken);
        var token = _tokenGenerator.CreateToken(user.Username);
        return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
    }

    public async Task<UserDto> UpdateAsync(
        string username, UpdatedUserDto updatedUser, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByUsernameAsync(username, cancellationToken);
        user.UpdateUser(updatedUser);
        await _repository.SaveChangesAsync(cancellationToken);
        var token = _tokenGenerator.CreateToken(user.Username);
        return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
    }

    public async Task<UserDto> LoginAsync(LoginUserDto login, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(login.Email);

        if (user == null || user.Password != login.Password)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Incorrect Credentials",
                Errors = { new KeyValuePair<string, string[]>("Credentials", new[] { "incorrect credentials" }) }
            });
        }

        var token = _tokenGenerator.CreateToken(user.Username);
        return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
    }

    public async Task<UserDto> GetAsync(string username, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByUsernameAsync(username, cancellationToken);
        var token = _tokenGenerator.CreateToken(user.Username);
        return new UserDto(user.Username, user.Email, token, user.Bio, user.Image);
    }
}
