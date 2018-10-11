using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Models;
using SeeYouIn.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using Unity;

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
        OnPropertyChanged();
      }
    }

    public IReminderService ReminderService { get; set; }

    public MyRemindersViewModel()
    {
      ReminderService = Injector.Container.Resolve<IReminderService>();
      SetPropertiesAsync();
    }

    public async void SetPropertiesAsync()
    {
      ReminderList = await ReminderService.GetRemindersAsync();
      HasReminders = ReminderList.Any();
    }
  }
}
