namespace realworlddotnet.Domain.Entities
{
    public class FollowedPerson
    {
        public FollowedPerson(int observerId, User observer, int targetId, User target)
        {
            ObserverId = observerId;
            Observer = observer;
            TargetId = targetId;
            Target = target;
        }

        public int ObserverId { get; set; }
        public User Observer { get; set; }

        public int TargetId { get; set; }
        public User Target { get; set; }
    }
}