using Microsoft.EntityFrameworkCore;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.LocalDB.Context;
using SeeYouIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeYouIn.LocalDB.Repositories
{
  public class NotificationLinkRepository : INotificationLinkLocalService
  {
    private readonly SeeYouDbContext _databaseContext;

    public NotificationLinkRepository()
    {
      _databaseContext = new SeeYouDbContext();
    }

    public async Task<List<NotificationLink>> GetNotificationLinksAsync()
    {
      try
      {
        var notificationLinks = await _databaseContext.NotificationLinks.ToListAsync();

        return notificationLinks;
      }
      catch (Exception)
      {
        return null;
      }
    }

    public async Task<List<NotificationLink>> GetNotificationLinksAsync(int reminderID)
    {
      try
      {
        var notificationLinks = (await _databaseContext.NotificationLinks.ToListAsync()).Where(n => n.ReminderID == reminderID).ToList();
        return notificationLinks;
      }
      catch (Exception ee)
      {
        return null;
      }
    }

    public async Task<bool> InsertNotificationLinkAsync(NotificationLink notificationLink)
    {
      bool result = false;
      try
      {
        var tracking = _databaseContext.Add(notificationLink);
        await _databaseContext.SaveChangesAsync();
        result = tracking.State == EntityState.Added;
        return result;
      }
      catch (Exception)
      {
        return result;
      }
    }

    public async Task<bool> RemoveNotificationLinkAsync(NotificationLink notificationLink)
    {
      bool result = false;
      try
      {
        var tracking = _databaseContext.NotificationLinks.Remove(notificationLink);
        await _databaseContext.SaveChangesAsync();
        result = tracking.State == EntityState.Deleted;
        return result;
      }
      catch (Exception)
      {
        return result;
      }
    }
  }
}
