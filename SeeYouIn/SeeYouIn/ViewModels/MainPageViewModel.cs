using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.Models;
using SeeYouIn.Services;
using SeeYouIn.ViewModels.Base;
using SeeYouIn.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace SeeYouIn.ViewModels
{
  public class MainPageViewModel : BaseViewModel
  {
    private ObservableCollection<Reminder> reminders = new ObservableCollection<Reminder>();
    public ObservableCollection<Reminder> Reminders
    {
      get => reminders;
      set
      {
        reminders = value;
        OnPropertyChanged();
      }
    }

    private IReminderLocalService ReminderService;
    private IReminderService NotificationService;

    public MainPageViewModel()
    {
      ReminderService = Injector.Container.Resolve<IReminderLocalService>();
      NotificationService = Injector.Container.Resolve<IReminderService>();

      InitializeCommand.Execute(null);
    }

    private async Task PopulateRemindersAsync()
    {
      if (ReminderService == null)
      {
        Debug.WriteLine("ReminderService not injected on MainPageViewModel");
      }
      else
      {
        Reminders = await ReminderService.GetRemindersAsync();
      }
    }

    public ICommand InitializeCommand => new Command(async () =>
    {
      await PopulateRemindersAsync();
    });

    public ICommand AddReminderCommand => new Command(() =>
   {
   });

  }
}
