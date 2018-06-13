
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinNativeTemplate.Droid.Activities
{
    [Activity(Label = "DetailedActivity")]
    public class DetailedActivity : Activity
    {
        TextView name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetailedLayout);

            name = FindViewById<TextView>(Resource.Id.empNameTextView);
            var email = FindViewById<TextView>(Resource.Id.empEmailTextView);
            var phone = FindViewById<TextView>(Resource.Id.empPhoneTextView);
            var dob = FindViewById<TextView>(Resource.Id.empDOBTextView);

            var crashButton = FindViewById<Button>(Resource.Id.empCrashButton);

            name.Text = "zzz";
            email.Text = "zzz";
            phone.Text = "zzz";
            dob.Text = "zzz";
            crashButton.Click+= CrashButton_Click;
        }

        public void CrashButton_Click(object sender, EventArgs e)
        {
            name = null;
            name.Text = "crahed";
        }

    }
}
