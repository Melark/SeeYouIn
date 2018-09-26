using SeeYouIn.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SeeYouIn.Interfaces.LocalDB
{
    interface IReminderService
    {
        Task<List<Reminder>> GetRemindersAsync();

        Task<bool> InsertReminderAsync(Reminder reminder);

        Task<bool> UpdateReminderAsync(Reminder reminder);

        Task<bool> RemoveReminderAsync(Reminder reminder);

    }
}
