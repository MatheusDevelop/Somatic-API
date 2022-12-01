using Newtonsoft.Json;

namespace UI.Aws.ViewModels
{
    public class SequenceCreateViewModel
    {
        public int Id { get; set; }
        public int Series { get; set; }
        public int Repetitions { get; set; }
        public bool UntilFail { get; set; }
    }
    public class SequenceViewModel
    {
        public SequenceViewModel(int id, int series, int repetitions, bool untilFail, string name, string machineName, string? machineNumber, List<string> mediaUrls)
        {
            Id = id;
            Series = series;
            Repetitions = repetitions;
            UntilFail = untilFail;
            Name = name;
            MachineName = machineName;
            MachineNumber = machineNumber;
            MediaUrls = mediaUrls;
        }

        public int Id { get; set; }
        public int Series { get; set; }
        public int Repetitions { get; set; }
        public bool UntilFail { get; set; }
        public string Name { get; set; }
        public string MachineName { get; set; }
        public string? MachineNumber { get; set; } = null;
        public List<string> MediaUrls { get; set; }
    }
}
