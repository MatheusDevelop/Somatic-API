namespace Domain.Entities
{
    public class Leaner : User
    {
        public Leaner(string name, string userNick, string pass, string profilePictureUrl) : base(name, userNick, pass, profilePictureUrl)
        {
        }
        protected Leaner()
        {

        }
        public List<Workout> Workouts { get; private set; } = new();
        public void RemoveWorkout(int workoutId)
        {
            var workoutToRemove = Workouts.FirstOrDefault(e => e.Id == workoutId);
            if (workoutToRemove is not null)
                Workouts.Remove(workoutToRemove);
        }
    }
}