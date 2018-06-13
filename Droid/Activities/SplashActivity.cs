using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Support.V7.App;
using Plugin.CurrentActivity;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;

namespace XamarinNativeTemplate.Droid
{
    [Activity(Label = "XamarinNativeTemplate", Icon = "@mipmap/icon", Theme = "@style/Theme.Splash", MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
                return;

            UserDialogs.Init(() => CrossCurrentActivity.Current.Activity);
           
           // CrashManager.Register(this, "com.sil.XamarinNativeTemplate");
           
            // Enable User Metrics
           // MetricsManager.Register(Application, "com.sil.XamarinNativeTemplate");

           // DownloadEmployeeData();
            MainApplication.Locator.MainVm.BuildEmployeeList();

            IsGooglePlayServiceAvailable();

            var newIntent = new Intent(this, typeof(MyTableViewActivity));

            StartActivity(newIntent);

            Finish();
        }

        public override void OnBackPressed()
        {
            //Finish();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (IsFinishing)
                UserDialogs.Instance.Loading("").Dispose();
        }

        async void DownloadEmployeeData() => await MainApplication.Locator.MainVm.DownloadAllEmployeesData();

        public bool IsGooglePlayServiceAvailable()
        {
            try
            {
                int lResultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

                if (lResultCode != ConnectionResult.Success)
                {
                    if (GoogleApiAvailability.Instance.IsUserResolvableError(lResultCode))
                    {
                        string lstrResult = GoogleApiAvailability.Instance.GetErrorString(lResultCode);

                        UserDialogs.Instance.Alert(lstrResult, "Service unavailable", "Ok");
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("This device is not supported.", "Service unavailable", "Ok");

                        Finish();
                    }

                    return false;
                }

                UserDialogs.Instance.Alert("Google Play Services is available.", "Service available", "Ok");

                return true;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Error", "Ok");
            }

            return false;
        }
    }
}
