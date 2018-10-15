using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.LocalDB.Context;
using SeeYouIn.ViewModels.Base;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace SeeYouIn.ViewModels
{
  public class SettingsViewModel : BaseViewModel
  {

    IReminderService NotificationService { get; set; }
    IReminderLocalService ReminderService { get; set; }

    public SettingsViewModel()
    {
      NotificationService = Injector.Container.Resolve<IReminderService>();
      ReminderService = Injector.Container.Resolve<IReminderLocalService>();
    }

    public ICommand RemoveAllNotificationsCommand
    {
      get
      {
        return new Command(async () =>
        {
          NotificationService.CancelAllNotifications();
          var reminders = await ReminderService.GetRemindersAsync();
          foreach (var item in reminders)
          {
            await ReminderService.RemoveReminderAsync(item);
          }
        });
      }
    }

    public ICommand ResetDatabaseCommand
    {
      get
      {
        return new Command(() =>
        {
          SeeYouDbContext seeYouDbContext = new SeeYouDbContext();
          seeYouDbContext.ResetDB();
        });
      }
    }
  }
}
