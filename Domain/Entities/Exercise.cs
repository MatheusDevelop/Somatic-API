namespace Domain.Entities
{
    public class Exercise : Entity
    {
        protected Exercise()
        {
        }
        public Exercise(string name, User createdByUser, Machine machine, List<MediaUrl> mediaUrls)
        {
            Name = name;
            CreatedByUser = createdByUser;
            Machine = machine;
            MediaUrls = mediaUrls;
        }

        public string Name { get; private set; }
        public Machine Machine { get; set; }
        public List<MediaUrl> MediaUrls { get; private set; } = new();
        public User CreatedByUser { get; private set; }
        public List<Sequence> Sequences { get; set; }

        public void UpdateAll(Exercise exercise)
        {
            MediaUrls.Clear();
            Name = exercise.Name;
            Machine = exercise.Machine;
            MediaUrls = exercise.MediaUrls;
            CreatedByUser = exercise.CreatedByUser;
        }
    }
}