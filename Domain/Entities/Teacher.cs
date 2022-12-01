namespace Domain.Entities
{
    public class Teacher : User
    {
        public Teacher(string name, string userNick, string pass, string profilePictureUrl) : base(name, userNick, pass, profilePictureUrl)
        {
        }
        public List<Workout> CreatedWorkouts { get; set; } = new();
        protected Teacher()
        {

        }
    }
}