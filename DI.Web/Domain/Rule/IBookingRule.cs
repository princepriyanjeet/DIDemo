using DI.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Domain.Rule
{
    public interface ICourtBookingRule
    {
        Task<bool> CompliesWithRuleAsync(CourtBooking booking);

        string ErrorMessage { get; }
    }

    public interface IScopedCourtBookingRule : ICourtBookingRule
    {
    }

    public interface ISingletonCourtBookingRule : ICourtBookingRule
    {
    }
}
