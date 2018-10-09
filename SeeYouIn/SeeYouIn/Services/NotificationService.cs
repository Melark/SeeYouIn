using Plugin.Notifications;
using SeeYouIn.Interfaces.Notifications;
using System;
using System.Threading.Tasks;

namespace SeeYouIn.Services
{
    public class NotificationService : INotificationService
    {
        public async Task<int> RegisterNotification(string title, string message, DateTime dateTime)
        {
            TimeSpan timeSpan = new TimeSpan();

             await CrossNotifications.Current.Send(new Notification
             {
                 Title = title,
                 Message = message,
                 Vibrate = true,
                 When = timeSpan
             });
        }
    }
}
