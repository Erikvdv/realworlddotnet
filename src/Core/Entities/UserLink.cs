namespace Realworlddotnet.Core.Entities;

public class UserLink
{
    public UserLink(string username, string followerUsername)
    {
        Username = username;
        FollowerUsername = followerUsername;
    }

    public string Username { get; set; }

    public string FollowerUsername { get; set; }

    public User User { get; set; } = null!;
    public User FollowerUser { get; set; } = null!;
}
