namespace UI.Aws.ViewModels
{
    public class ExerciseCreateViewModel
    {
        public string Name { get; set; }
        public int MachineId { get; set; }
        public int UserId { get; set; }
        public List<string> MediaUrls { get; set; }
    }
    public class ExerciseSearchViewModel 
    {
        public ExerciseSearchViewModel(int id, string name, string machineName, string? machineNumber, string assignedUserName, string assignedUserProfilePictureUrl, int machineId, List<string> mediaUrls)
        {
            Id = id;
            Name = name;
            MachineName = machineName;
            MachineNumber = machineNumber;
            AssignedUserName = assignedUserName;
            AssignedUserProfilePictureUrl = assignedUserProfilePictureUrl;
            MachineId = machineId;
            MediaUrls = mediaUrls;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MachineId { get; set; }
        public List<string> MediaUrls { get; set; }
        public string MachineName { get; set; }
        public string AssignedUserName { get; set; }
        public string AssignedUserProfilePictureUrl { get; set; }
        public string? MachineNumber { get; set; } = null;
    }

}
