using System.Threading.Tasks;

namespace SeeYouIn.Interfaces.Notifications
{
  public interface INotificationService
  {
    Task<int> RegisterNotification(Models.Notification notification);

    void CancelAllNotifications();

  }
}
