using System.Threading.Tasks;

namespace SeeYouIn.Interfaces.Notifications
{
  public interface IReminderService
  {
    Task<bool> RegisterNotification(Models.Notification notification, bool isReminderAlreadyInserted = false);

    void CancelAllNotifications();

    void CancelNotification(int reminderID);

    Task<bool> UpdateNotification(Models.Notification notification);
  }
}
