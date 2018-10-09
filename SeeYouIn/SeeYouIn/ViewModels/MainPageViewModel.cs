using Plugin.Notifications;
using SeeYouIn.DI;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Models;
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

        private IReminderService ReminderService;

        public MainPageViewModel()
        {
            ReminderService = Injector.Container.Resolve<IReminderService>();
            InitializeCommand.Execute(null);
            TestNotifications();
        }

        private  void TestNotifications() {

            for (int i = 0; i < 365; i++)
            {
                
            }
        }

        private async Task PopulateRemindersAsync()
        {
            if (ReminderService == null)
            {
                Debug.WriteLine("ReminderService not injected on MainPageViewModel");
            }
            else
            {
                var reminders = await ReminderService.GetRemindersAsync();
                if (!PopulateObservableList(reminders))
                {
                    Debug.WriteLine("No reminders found in DB");
                }
            }
        }

        private bool PopulateObservableList(List<Reminder> reminderParams)
        {
            bool populateResult = true;
            foreach (var reminder in reminderParams)
            {
                Reminders.Add(reminder);
            }
            return populateResult;
        }

        public ICommand InitializeCommand => new Command(async () =>
        {
            await PopulateRemindersAsync();
        });

        public ICommand AddReminderCommand => new Command( () =>
        {
            Application.Current.MainPage.Navigation.PushAsync(new AddReminderPage());
        });

    }
}
