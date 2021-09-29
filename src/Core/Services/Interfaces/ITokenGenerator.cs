namespace realworlddotnet.Core.Services.Interfaces
{
    public interface ITokenGenerator
    {
        public string CreateToken(string username);
    }
}