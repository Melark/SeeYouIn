using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.ViewModels.Base;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace SeeYouIn.ViewModels
{
  public class SettingsViewModel : BaseViewModel
  {

    INotificationService NotificationService { get; set; }
    IReminderService ReminderService { get; set; }

    public SettingsViewModel()
    {
      NotificationService = Injector.Container.Resolve<INotificationService>();
      ReminderService = Injector.Container.Resolve<IReminderService>();
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
  }
}
