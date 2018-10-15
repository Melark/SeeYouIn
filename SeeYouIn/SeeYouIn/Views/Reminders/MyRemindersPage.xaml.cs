using SeeYouIn.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeeYouIn.Views.Reminders
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MyRemindersPage : ContentPage
  {
    public MyRemindersPage()
    {
      InitializeComponent();
    }

    protected override void OnAppearing()
    {
      // TODO: Move logic to behaviors or something other than this
      base.OnAppearing();
      ViewModel.ReloadRemindersCommand.Execute(null);
    }

    public MyRemindersViewModel ViewModel
    {
      get
      {
        return BindingContext as MyRemindersViewModel;
      }
    }
  }
}