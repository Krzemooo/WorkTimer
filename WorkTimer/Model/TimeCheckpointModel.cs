using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.Model
{
    /// <summary>
    /// Enum statusu eventu
    /// </summary>
    public enum TimeCheckpoinStatus
    {
        Start = 1,
        Break = 2
    }
    /// <summary>
    /// Model danych odpowiadający za pojedyńczy event w pliku
    /// </summary>
    public class TimeCheckpointModel
    {
        public DateTime date { get; set; }
        public TimeCheckpoinStatus status { get; set; }

    }
}
