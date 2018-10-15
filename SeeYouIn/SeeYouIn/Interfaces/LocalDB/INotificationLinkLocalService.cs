using SeeYouIn.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeeYouIn.Interfaces.LocalDB
{
  public interface INotificationLinkLocalService
  {
    Task<List<NotificationLink>> GetNotificationLinksAsync();

    Task<List<NotificationLink>> GetNotificationLinksAsync(int reminderID);

    Task<bool> InsertNotificationLinkAsync(NotificationLink notificationLink);

    Task<bool> RemoveNotificationLinkAsync(NotificationLink notificationLink);

  }
}
