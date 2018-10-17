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
                    new MainPageMenuItem { Id = 0, Title = "My Reminders" ,TargetType = typeof(MyRemindersPage), FontAwesomeIconText = "\uf073"},
                    new MainPageMenuItem { Id = 1, Title = "Settings",TargetType = typeof(SettingsPage), Enabled = false , FontAwesomeIconText = "\uf4fe"},
                    new MainPageMenuItem { Id = 2, Title = "About" ,TargetType = typeof(AboutPage), FontAwesomeIconText = "\uf059"}
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