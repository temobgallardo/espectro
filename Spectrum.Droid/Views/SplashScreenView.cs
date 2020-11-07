
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Spectrum.Droid.Views
{
    [Activity(
        Label = "@string/app_name"
        , MainLauncher = true
        , NoHistory = true
        , Theme = "@style/AppTheme.Splash"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenView : MvxSplashScreenAppCompatActivity
    {
        public SplashScreenView()
           : base(Resource.Layout.activity_splash_screen)
        { }
    }
}