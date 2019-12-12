using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geldquelle
{
    public class Öffnungszeiten
    {
        public bool IsOpen(DateTime today)
        {
            TimeSpan open = new TimeSpan(10, 30, 00);
            TimeSpan closed = new TimeSpan(19, 00, 00);

            switch (today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return (today.TimeOfDay >= open && today.TimeOfDay < closed);
                case DayOfWeek.Saturday:
                    closed = new TimeSpan(14, 00, 00);
                    return (today.TimeOfDay >= open && today.TimeOfDay < closed);
                default:// case DayOfWeek.Sunday:
                    return false;
            }
        }
    }
}
