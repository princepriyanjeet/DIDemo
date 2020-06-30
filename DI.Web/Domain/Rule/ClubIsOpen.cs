using DI.Web.Configuration;
using DI.Web.Data;
using System.Threading.Tasks;

namespace DI.Web.Domain.Rule
{
    public class ClubIsOpen : ISingletonCourtBookingRule
    {
        private readonly IClubConfiguration _clubConfiguration;

        public string ErrorMessage => "Can't make a booking when the club is closed";
        public ClubIsOpen(IClubConfiguration clubConfiguration)
        {
            this._clubConfiguration = clubConfiguration;
        }

        public Task<bool> CompliesWithRuleAsync(CourtBooking booking)
        {
            var startHourPasses = booking.StartDateTime.Hour >= _clubConfiguration.OpenHour;
            var endHourPasses = booking.EndDateTime.Hour <= _clubConfiguration.CloseHour;

            return Task.FromResult(startHourPasses && endHourPasses);
        }
    }
}
