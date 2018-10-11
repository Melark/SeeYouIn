using Plugin.Notifications;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.Models;
using System;
using System.Threading.Tasks;

namespace SeeYouIn.Services
{
  public class NotificationService : INotificationService
  {
    public async void CancelAllNotifications()
    {
      await CrossNotifications.Current.CancelAll();
    }

    public async Task<int> RegisterNotification(Models.Notification notification)
    {
      TimeSpan timeSpan = new TimeSpan();

      timeSpan = notification.UntilDate - DateTime.Now;

      // Todo: Extract to method that takes NotificationFrequency and builds a notification list based on that
      if (notification.FrequencyToSend == Enums.NotificationFrequency.DAILY)
      {
        for (int i = 0; i < timeSpan.Days; i++)
        {
          await CrossNotifications.Current.Send(new Plugin.Notifications.Notification
          {
            Title = notification.Title,
            Message = notification.Body,
            Vibrate = true,
            When = TimeSpan.FromMinutes(i)
          });
        }
      }

      return 1;
    }
  }
}
