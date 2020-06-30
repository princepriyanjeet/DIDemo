using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Configuration
{
    public interface IClubConfiguration
    {
        int CloseHour { get; set; }
        int OpenHour { get; set; }
        int PeakEndHour { get; set; }
        int PeakStartHour { get; set; }
        int WinterCourtEndHour { get; set; }
        int WinterCourtStartHour { get; set; }
        int[] WinterMonths { get; set; }
    }
}
