using SeeYouIn.Models;
using SeeYouIn.Views.Assisting;
using SeeYouIn.Views.Reminders;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeeYouIn.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MainPageMaster : ContentPage
  {
    public ListView ListView;

    public MainPageMaster()
    {
      InitializeComponent();

      BindingContext = new MainPageMasterViewModel();
      ListView = MenuItemsListView;
    }

    class MainPageMasterViewModel : INotifyPropertyChanged
    {
      public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

      public MainPageMasterViewModel()
      {
        MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
        {
                    new MainPageMenuItem { Id = 0, Title = "My Reminders" ,TargetType = typeof(MyRemindersPage)},
                    new MainPageMenuItem { Id = 1, Title = "Add Reminders" ,TargetType = typeof(AddRemindersPage)},
                    new MainPageMenuItem { Id = 2, Title = "Settings",TargetType = typeof(SettingsPage), Enabled = false },
                    new MainPageMenuItem { Id = 3, Title = "About" ,TargetType = typeof(AboutPage)}
                });
      }

      #region INotifyPropertyChanged Implementation
      public event PropertyChangedEventHandler PropertyChanged;
      void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
        if (PropertyChanged == null)
          return;

        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion
    }
  }
}