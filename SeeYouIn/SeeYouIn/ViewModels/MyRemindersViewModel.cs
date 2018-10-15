using Rg.Plugins.Popup.Services;
using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.Models;
using SeeYouIn.ViewModels.Base;
using SeeYouIn.Views.Reminders;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace SeeYouIn.ViewModels
{
  public class MyRemindersViewModel : BaseViewModel
  {
    private bool shouldShowNoRemindersMessage;
    public bool ShouldShowNoRemindersMessage
    {
      get => shouldShowNoRemindersMessage;
      set
      {
        shouldShowNoRemindersMessage = value;
        OnPropertyChanged();
      }
    }

    private bool hasReminders;
    public bool HasReminders
    {
      get => hasReminders;
      set
      {
        hasReminders = value;
        ShouldShowNoRemindersMessage = !value;
        OnPropertyChanged();
      }
    }

    private ObservableCollection<Reminder> reminderList;
    public ObservableCollection<Reminder> ReminderList
    {
      get => reminderList;
      set
      {
        reminderList = value;
        ShouldShowNoRemindersMessage = reminderList.Any() ? true : false;
        OnPropertyChanged();
      }
    }

    private IReminderService NotificationService;
    public IReminderLocalService ReminderService { get; set; }

    public MyRemindersViewModel()
    {
      NotificationService = Injector.Container.Resolve<IReminderService>();
      ReminderService = Injector.Container.Resolve<IReminderLocalService>();
      SetPropertiesAsync();
    }

    private async void SetPropertiesAsync()
    {
      ReminderList = await ReminderService.GetRemindersAsync();
      HasReminders = ReminderList.Any();
    }


    public ICommand AddReminderCommand
    {
      get
      {
        return new Command(async () =>
        {
          MessagingCenter.Subscribe<AddReminderPageViewModel>(this, "ReminderAdded", (sender) =>
          {
            ReloadRemindersCommand.Execute(null);
            MessagingCenter.Unsubscribe<AddReminderPageViewModel>(this, "ReminderAdded");
          });
          await PopupNavigation.Instance.PushAsync(new AddRemindersPage());

        });
      }
    }

    public ICommand RemoveReminderCommand
    {
      get
      {
        return new Command(async (r) =>
        {
          var alertResult = await Application.Current.MainPage.DisplayAlert("Caution", "Are you sure you want to remove this reminder?", "YES", "NO");
          if (alertResult)
          {
            Reminder reminder = (Reminder)r;
            ReminderList.Remove(reminder);
            await ReminderService.RemoveReminderAsync(reminder);
          }

        });
      }
    }

    public ICommand EditReminderCommand
    {
      get
      {
        return new Command((r) =>
        {
          Reminder reminder = (Reminder)r;

          Application.Current.MainPage.Navigation.PushModalAsync(new EditReminderPage(reminder));
        });
      }
    }

    public ICommand ReloadRemindersCommand
    {
      get
      {
        return new Command(() =>
        {
          SetPropertiesAsync();
        });
      }
    }


  }
}
