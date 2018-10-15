﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace SeeYouIn.Droid
{
  [Activity(Label = "MVP",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/MyTheme.Splash",
        MainLauncher = true)]
  public class SplashActivity : AppCompatActivity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      var intent = new Intent(this, typeof(MainActivity));
      StartActivity(intent);
      Finish();
    }
  }
}