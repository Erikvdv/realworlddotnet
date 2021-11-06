namespace Realworlddotnet.Core.Entities;

public class FollowedUser
{
    public string Username { get;  set; }
    
    public string FollowerUsername { get;  set; }

    public User User { get;  set; } = null!;
    public User FollowerUser { get;  set; } = null!;
    
    public FollowedUser(string username, string followerUsername)
    {
        Username = username;
        FollowerUsername = followerUsername;
    }
    
}
