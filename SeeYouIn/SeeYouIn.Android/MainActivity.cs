
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SeeYouIn.Droid
{
  [Activity(Label = "SeeYouIn", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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