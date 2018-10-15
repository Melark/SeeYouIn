
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.Forms.Platform.Android;

namespace SeeYouIn.Droid
{
  [Activity(Label = "SeeYouIn",Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {

      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;
      SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
      base.OnCreate(savedInstanceState);

      Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
      LoadApplication(new App());

      Window window = this.Window;
      window.ClearFlags(WindowManagerFlags.TranslucentStatus);
      window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
      Xamarin.Forms.Color xfColor = (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["SecondaryColor"];
      Android.Graphics.Color color = xfColor.ToAndroid();
      window.SetStatusBarColor(color);
    }


    public override void OnBackPressed()
    {
      if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
      {
        // Do something if there are some pages in the `PopupStack`
      }
      else
      {
        //base.OnBackPressed();
      }
    }
  }
}