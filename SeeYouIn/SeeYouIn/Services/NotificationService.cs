using Plugin.Notifications;
using SeeYouIn.Interfaces.Notifications;
using System;

namespace SeeYouIn.Services
{
  public class NotificationService : INotificationService
  {
    public async void SendSingleNotification(Models.Notification notification)
    {
      TimeSpan ts = notification.UntilDate.Subtract(DateTime.Now);
      int dayCount = ts.Days;

      await CrossNotifications.Current.Send(new Notification()
      {
        Title = notification.Title,
        Message = $"{notification.Body} ({dayCount} days)",
        When = TimeSpan.FromSeconds(0)
      });
    }
  }
}
