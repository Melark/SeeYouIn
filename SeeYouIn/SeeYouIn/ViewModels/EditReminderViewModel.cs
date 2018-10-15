using SeeYouIn.DI;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.Models;
using SeeYouIn.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;
using Unity;

namespace SeeYouIn.ViewModels
{
  public class EditReminderViewModel : BaseViewModel
  {
    private IReminderService ReminderService { get; set; }

    public EditReminderViewModel()
    {
      ReminderService = Injector.Container.Resolve<IReminderService>();
    }

    private Reminder reminderToEdit;
    public Reminder ReminderToEdit
    {
      get => reminderToEdit;
      set
      {
        reminderToEdit = value;
        OnPropertyChanged();
      }
    }

    public ICommand SaveCommand
    {
      get
      {
        return new Command(() =>
        {
          Notification notificationToUpdate = new Notification(ReminderToEdit);
          ReminderService.UpdateNotification(notificationToUpdate);
        });
      }
    }

    public ICommand CancelCommand
    {
      get
      {
        return new Command(() =>
        {
          Application.Current.MainPage.Navigation.PopModalAsync();
        });
      }
    }
  }
}
