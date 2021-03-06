﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DevExpress.Mobile.DataGrid.Theme;

namespace Core.App.Droid
{
    [Activity(Label = "Inventario", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);           

            global::Xamarin.Forms.Forms.Init(this, bundle);
            DevExpress.Mobile.Forms.Init();
            ThemeManager.ThemeName = Themes.Light;
            LoadApplication(new App());
        }
    }
}

