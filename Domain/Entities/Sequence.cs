using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sequence : Entity
    {
        protected Sequence()
        {

        }
        public Sequence(Exercise exercise, int series, int repetitions, bool untilFail, int order)
        {
            Exercise = exercise;
            Series = series;
            Repetitions = repetitions;
            UntilFail = untilFail;
            Order = order;
        }
        public int Order { get; private set; }
        public Exercise Exercise { get; private set; }
        public int Series { get; private set; }
        public int Repetitions { get; private set; }
        public bool UntilFail { get; private set; }
    }
}
