using System;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Services.Notifications
{
    public interface INotificationService
    {
        Task SendAsync(string message, string userId);
    }
}
