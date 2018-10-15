using SeeYouIn.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SeeYouIn.Interfaces.LocalDB
{
  public interface IReminderLocalService
  {
    Task<ObservableCollection<Reminder>> GetRemindersAsync();

    Task<bool> InsertReminderAsync(Reminder reminder);

    Task<bool> UpdateReminderAsync(Reminder reminder);

    Task<bool> RemoveReminderAsync(Reminder reminder);

  }
}
