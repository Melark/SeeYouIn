using Rg.Plugins.Popup.Services;
using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.Models;
using SeeYouIn.ValidationRules;
using SeeYouIn.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace SeeYouIn.ViewModels
{
  public class AddReminderPageViewModel : BaseViewModel
  {
    #region Services
    private IReminderService ReminderService;
    private INotificationService NotificationService;
    #endregion

    #region Properties

    public List<FrequencyItem> Frequency{get;}

    private ValidatableObject<string> reminderText = new ValidatableObject<string>();
    public ValidatableObject<string> ReminderText
    {
      get => reminderText;
      set
      {
        reminderText = value;
        OnPropertyChanged();
      }
    }

    private ValidatableObject<string> reminderTitle = new ValidatableObject<string>();
    public ValidatableObject<string> ReminderTitle
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
    #endregion

    public AddReminderPageViewModel()
    {
      ReminderService = Injector.Container.Resolve<IReminderService>();
      NotificationService = Injector.Container.Resolve<INotificationService>();
      ReminderDate = DateTime.Now;
      AddPropertyValidations();

      Frequency = new List<FrequencyItem>() {
        new FrequencyItem(){ Frequency = Enums.NotificationFrequency.MONTHLY, Value = "Monthly"},
        new FrequencyItem(){ Frequency = Enums.NotificationFrequency.DAILY, Value = "Daily"}
      };
    }
    #region Commands

    public ICommand AddCommand => new Command(async () =>
    {
      if (ValidateProperties())
      {
        try
        {
          Notification notification = new Notification(ReminderTitle.Value, ReminderText.Value, ReminderDate, Enums.NotificationFrequency.DAILY);

          await ReminderService.RegisterNotification(new Notification(ReminderTitle.Value, ReminderText.Value, ReminderDate, Enums.NotificationFrequency.DAILY));

          CancelCommand.Execute(null);
        }
        catch (Exception ee)
        {
          return;
        }
      }
    });

    public ICommand CancelCommand => new Command(async () =>
    {
      await PopupNavigation.Instance.PopAsync();
      MessagingCenter.Send<AddReminderPageViewModel>(this, "ReminderAdded");
    });
    #endregion

    #region Functions

    private bool ValidateProperties()
    {
      return ReminderTitle.Validate() && ReminderText.Validate();
    }

    private void AddPropertyValidations()
    {
      ReminderTitle.Validations.Add(new IsNotNullOrEmptyRule<string>
      {
        ValidationMessage = "A title is required."
      });

      ReminderText.Validations.Add(new IsNotNullOrEmptyRule<string>
      {
        ValidationMessage = "A message is required"
      });
    }

    public ICommand PreviewCommand
    {
      get
      {
        return new Command(() =>
        {
          if (ValidateProperties())
          {
            try
            {
              Notification notification = new Notification(ReminderTitle.Value, ReminderText.Value, ReminderDate, Enums.NotificationFrequency.DAILY);
              NotificationService.SendSingleNotification(notification);
            }
            catch (Exception ee)
            {
              return;
            }
          }
          
        });
      }
    }
    #endregion
  }
}
