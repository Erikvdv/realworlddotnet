namespace realworlddotnet.Domain.Entities
{
    public class FollowedPerson
    {
        public int ObserverId { get; set; }
        public User Observer { get; set; }

        public int TargetId { get; set; }
        public User Target { get; set; }
    }
}