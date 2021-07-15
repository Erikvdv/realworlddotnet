namespace realworlddotnet.Domain.Services.Interfaces
{
    public interface ITokenGenerator
    {
        public string CreateToken(string username);
    }
}