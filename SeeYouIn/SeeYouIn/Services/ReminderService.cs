using Plugin.Notifications;
using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.Models;
using System;
using System.Threading.Tasks;
using Unity;

namespace SeeYouIn.Services
{
  public class ReminderService : IReminderService
  {

    private IReminderLocalService ReminderLocalDbService { get; set; }
    private INotificationLinkLocalService NotificationLinkLocalService { get; set; }

    public ReminderService()
    {
      ReminderLocalDbService = Injector.Container.Resolve<IReminderLocalService>();
      NotificationLinkLocalService = Injector.Container.Resolve<INotificationLinkLocalService>();
    }

    public async void CancelAllNotifications()
    {
      await CrossNotifications.Current.CancelAll();
    }

    public async Task<bool> RegisterNotification(Models.Notification notification, bool isReminderAlreadyInserted = false)
    {
      try
      {
        Reminder reminder = new Reminder();
        reminder.Body = notification.Body;
        reminder.Title = notification.Title;
        reminder.ETA = notification.UntilDate;

        if (!isReminderAlreadyInserted)
        {
          await ReminderLocalDbService.InsertReminderAsync(reminder);

          notification.ReminderID = reminder.ID;
        }
        else
        {
          reminder.ID = notification.ReminderID;
        }

        TimeSpan timeSpan = new TimeSpan();
        timeSpan = notification.UntilDate.Subtract(DateTime.Now);



        // Todo: Extract to method that takes NotificationFrequency and builds a notification list based on that
        if (notification.FrequencyToSend == Enums.NotificationFrequency.DAILY)
        {
          for (int i = 0; i < timeSpan.Days; i++)
          {
            Plugin.Notifications.Notification notificationObject = new Plugin.Notifications.Notification
            {
              Title = notification.Title,
              Message = $"{notification.Body} ({timeSpan.Days - i} Day(s))",
              Vibrate = true,
              When = TimeSpan.FromDays(i)
            };
            await CrossNotifications.Current.Send(notificationObject);

            if (notificationObject.Id != 0)
            {
              NotificationLink notificationLink = new NotificationLink();
              notificationLink.ReminderID = reminder.ID;
              notificationLink.NotificationID = notificationObject.Id ?? 0;

              var a = await NotificationLinkLocalService.InsertNotificationLinkAsync(notificationLink);
            }
            else
            {
              return false;
            }
          }
        }
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }


    public async Task<bool> UpdateNotification(Models.Notification notification)
    {
      try
      {
        Reminder reminder = new Reminder();
        reminder.ID = notification.ReminderID;
        reminder.Body = notification.Body;
        reminder.Title = notification.Title;
        reminder.ETA = notification.UntilDate;

        await ReminderLocalDbService.UpdateReminderAsync(reminder);

        CancelNotification(reminder.ID);

        await RegisterNotification(notification, true);

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    private Models.Notification GetNewNotificationObject(Models.Notification notification)
    {
      return new Models.Notification(notification.Title, notification.Body, notification.UntilDate, notification.FrequencyToSend, notification.ReminderID);
    }

    public async void CancelNotification(int reminderID)
    {
      var notificationLinks = await NotificationLinkLocalService.GetNotificationLinksAsync(reminderID);

      foreach (var link in notificationLinks)
      {
        await CrossNotifications.Current.Cancel(link.NotificationID);
      }
    }
  }
}
