using Microsoft.EntityFrameworkCore;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.LocalDB.Context;
using SeeYouIn.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SeeYouIn.LocalDB.Repositories
{
  public class ReminderRepository : IReminderLocalService
  {
    private readonly SeeYouDbContext _databaseContext;

    public ReminderRepository()
    {
      _databaseContext = new SeeYouDbContext();
    }

    public async Task<ObservableCollection<Reminder>> GetRemindersAsync()
    {
      try
      {
        var reminders = await _databaseContext.Reminders.ToListAsync();
        ObservableCollection<Reminder> observableReminders = new ObservableCollection<Reminder>();
        foreach (var item in reminders)
        {
          observableReminders.Add(item);
        }
        return observableReminders;
      }
      catch (Exception)
      {
        return null;
      }
    }

    public async Task<bool> InsertReminderAsync(Reminder reminder)
    {
      bool result = false;
      try
      {
        var tracking = _databaseContext.Add(reminder);
        await _databaseContext.SaveChangesAsync();
        result = tracking.State == EntityState.Added;
        return result;
      }
      catch (Exception)
      {
        return result;
      }
    }

    public async Task<bool> RemoveReminderAsync(Reminder reminder)
    {
      bool result = false;
      try
      {
        var tracking = _databaseContext.Reminders.Remove(reminder);
        await _databaseContext.SaveChangesAsync();
        result = tracking.State == EntityState.Deleted;
        return result;
      }
      catch (Exception)
      {
        return result;
      }
    }

    public async Task<bool> UpdateReminderAsync(Reminder reminder)
    {
      bool result = false;
      try
      {
        var tracking = _databaseContext.Reminders.Update(reminder);
        await _databaseContext.SaveChangesAsync();
        result = tracking.State == EntityState.Modified;
        return result;
      }
      catch (Exception)
      {
        return result;
      }
    }
  }
}
