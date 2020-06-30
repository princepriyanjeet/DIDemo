using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Configuration
{
    public interface IBookingConfiguration
    {
        int MaxPeakBookingLengthInHours { get; set; }
        int MaxRegularBookingLengthInHours { get; set; }
    }
}
