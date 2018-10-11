using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.Models;
using SeeYouIn.ViewModels.Base;
using System;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace SeeYouIn.ViewModels
{
  public class AddReminderPageViewModel : BaseViewModel
  {
    private IReminderService ReminderService;
    private INotificationService NotificationService;

    private string reminderText;
    public string ReminderText
    {
      get => reminderText;
      set
      {
        reminderText = value;
        OnPropertyChanged();
      }
    }

    private string reminderTitle;
    public string ReminderTitle
    {
      get { return reminderTitle; }
      set
      {
        reminderTitle = value;
        OnPropertyChanged();
      }
    }

    private DateTime reminderDate;
    public DateTime ReminderDate
    {
      get => reminderDate;
      set
      {
        reminderDate = value;
        OnPropertyChanged();
      }
    }

    public AddReminderPageViewModel()
    {
      ReminderService = Injector.Container.Resolve<IReminderService>();
      NotificationService = Injector.Container.Resolve<INotificationService>();
      ReminderDate = DateTime.Now;
    }

    public ICommand AddCommand => new Command(async () =>
    {
      Reminder reminder = new Reminder();
      reminder.Title = ReminderTitle;
      reminder.Body = ReminderText;
      reminder.ETA = ReminderDate;

      await ReminderService.InsertReminderAsync(reminder);
      await NotificationService.RegisterNotification(new Notification(ReminderTitle, ReminderText, ReminderDate, Enums.NotificationFrequency.DAILY));

      ReminderText = "";
      ReminderText = "";
    });

    public ICommand CancelCommand => new Command(() =>
    {
    });
  }
}
