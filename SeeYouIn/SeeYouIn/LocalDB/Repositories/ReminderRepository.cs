using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.LocalDB.Context;
using SeeYouIn.Models;

namespace SeeYouIn.LocalDB.Repositories
{
    public class ReminderRepository : IReminderService
    {
        private readonly SeeYouDbContext _databaseContext;

        public ReminderRepository()
        {
            _databaseContext = new SeeYouDbContext();
        }

        public async Task<List<Reminder>> GetRemindersAsync()
        {
            try
            {
                var reminders = await _databaseContext.Reminders.ToListAsync();
                return reminders;
            }
            catch (Exception e)
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
            catch (Exception e)
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
