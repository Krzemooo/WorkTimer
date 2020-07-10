using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.Model
{
    /// <summary>
    /// Model danych czasu przepracowanego wg dnia.
    /// </summary>
    public class UserWorkTime
    {
        public DateTime DayStamp { get; set; }
        public TimeSpan DataWork { get; set; }
    }
}
