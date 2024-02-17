namespace Realworlddotnet.Core.Entities;

public class UserLink(string username, string followerUsername)
{
    public string Username { get; set; } = username;

    public string FollowerUsername { get; set; } = followerUsername;

    public User User { get; set; } = null!;
    public User FollowerUser { get; set; } = null!;
}
