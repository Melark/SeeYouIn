using SeeYouIn.Models;
using SeeYouIn.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeeYouIn.Views.Reminders
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class EditReminderPage : ContentPage
  {
    public EditReminderPage(Reminder reminderToEdit)
    {
      InitializeComponent();
      ViewModel.ReminderToEdit = reminderToEdit;
    }

    protected override bool OnBackButtonPressed()
    {
      ViewModel.CancelCommand.Execute(null);
      return true;
    }

    public EditReminderViewModel ViewModel
    {
      get
      {
        return BindingContext as EditReminderViewModel;
      }
    }
  }
}