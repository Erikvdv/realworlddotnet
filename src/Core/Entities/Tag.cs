namespace Realworlddotnet.Core.Entities;

public class Tag(string id)
{
    public string Id { get; set; } = id;

    public ICollection<Article> Articles { get; set; } = null!;
}
