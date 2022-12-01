namespace UI.Aws.ViewModels
{
    public class WorkoutSearchViewModel
    {
        public WorkoutSearchViewModel(string name, string assignedUserName, string assignedUserProfilePictureUrl, List<WorkoutLeanerSearchViewModel> leaners, int id)
        {
            Name = name;
            AssignedUserName = assignedUserName;
            AssignedUserProfilePictureUrl = assignedUserProfilePictureUrl;
            Leaners = leaners;
            Id = id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string AssignedUserName { get; set; }
        public string AssignedUserProfilePictureUrl { get; set; }
        public List<WorkoutLeanerSearchViewModel> Leaners { get; set; }
    }
    public class WorkoutViewModel 
    {
        public WorkoutViewModel(int id, string name, string assignedUserName, string assignedUserProfilePictureUrl, List<SequenceViewModel> sequences, List<WorkoutLeanerSearchViewModel> leaners, string? description)
        {
            Id = id;
            Name = name;
            AssignedUserName = assignedUserName;
            AssignedUserProfilePictureUrl = assignedUserProfilePictureUrl;
            Sequences = sequences;
            Leaners = leaners;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = null;
        public string AssignedUserName { get; set; }
        public string AssignedUserProfilePictureUrl { get; set; }
        public List<SequenceViewModel> Sequences { get; set; }
        public List<WorkoutLeanerSearchViewModel> Leaners { get; set; }

    }

    public class WorkoutLeanerSearchViewModel
    {
        public WorkoutLeanerSearchViewModel(string name, string profilePictureUrl, int id)
        {
            Name = name;
            ProfilePictureUrl = profilePictureUrl;
            Id = id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
    public class WorkoutCreateViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; } = null;
        public List<SequenceCreateViewModel> Sequences { get; set; }
        public List<int> LeanersIds { get; set; }
        public int UserId { get; set; }
    }
}
