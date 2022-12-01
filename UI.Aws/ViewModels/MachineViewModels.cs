namespace UI.Aws.ViewModels
{
    public class MachineCreateViewModel
    {
        public string Name { get; set; }
        public string? Number { get; set; } = null;
        public List<string> MediaUrls { get; set; }
    }
    public class MachineSearchViewModel
    {
        public MachineSearchViewModel(int id, string name, string? number, List<string> mediaUrls)
        {
            Id = id;
            Name = name;
            Number = number;
            MediaUrls = mediaUrls;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Number { get; set; } = null;
        public List<string> MediaUrls { get; set; }
    }
}
