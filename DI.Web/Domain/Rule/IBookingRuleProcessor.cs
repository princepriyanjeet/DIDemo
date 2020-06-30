using DI.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Domain.Rule
{
    public interface IBookingRuleProcessor
    {
        Task<(bool, IEnumerable<string>)> PassesAllRulesAsync(CourtBooking courtBooking);
    }

    public class BookingRuleProcessor : IBookingRuleProcessor
    {
        private readonly IEnumerable<ICourtBookingRule> _rules;

        public BookingRuleProcessor(IEnumerable<ICourtBookingRule> rules)
        {
            _rules = rules;
        }

        public async Task<(bool, IEnumerable<string>)> PassesAllRulesAsync(CourtBooking courtBooking)
        {
            var passedRules = true;

            var errors = new List<string>();

            foreach (var rule in _rules)
            {
                if (!await rule.CompliesWithRuleAsync(courtBooking))
                {
                    errors.Add(rule.ErrorMessage);
                    passedRules = false;
                }
            }

            return (passedRules, errors);
        }
    }
}
