namespace Realworlddotnet.Core.Entities;

public class Tag
{
    public Tag(string id)
    {
        Id = id;
    }

    public string Id { get; set; }

    public ICollection<Article> Articles { get; set; } = null!;
}
