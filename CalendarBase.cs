using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendaric
{
    public class CalendarBase
    {
        public DateTime date;
        public bool[] activities;

        public CalendarBase() {
            date = DateTime.Now;
            activities = new bool[5];
        }
    }
}
