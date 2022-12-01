using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Workout : Entity
    {
        protected Workout()
        {

        }
        public Workout(string name, string? description, Teacher createdBy, List<Sequence> sequences, List<Leaner> leaners)
        {
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            Sequences = sequences;
            Leaners = leaners;
        }
        public string Name { get; set; }
        public string? Description { get; private set; } = null;
        public List<Sequence> Sequences { get; private set; } = new();
        public List<Leaner> Leaners { get; private set; } = new();
        public Teacher CreatedBy { get; private set; }
        public void UpdateAllWorkout(string name, string? description, Teacher createdBy, List<Sequence> sequences, List<Leaner> leaners)
        {
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            Sequences = sequences;
            Leaners = leaners;
        }
    }
}
