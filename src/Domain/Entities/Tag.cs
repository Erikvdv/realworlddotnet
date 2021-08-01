namespace realworlddotnet.Domain.Entities
{
    public class Tag
    {
        public Tag(string tagId)
        {
            TagId = tagId;
        }

        public string TagId { get; set; }
    }
}