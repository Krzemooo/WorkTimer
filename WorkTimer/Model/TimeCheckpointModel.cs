using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.Model
{
    public enum TimeCheckpoinStatus
    {
        Start = 1,
        Break = 2
    }
    public class TimeCheckpointModel
    {
        public DateTime date { get; set; }
        public TimeCheckpoinStatus status { get; set; }

    }
}
