using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using HockeyApp.Android;
using XamarinNativeTemplate.Droid.Activities;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;


namespace XamarinNativeTemplate.Droid
{
    [Activity(Label = "MyTableViewActivity", MainLauncher = false, Theme = "@style/MyTheme",
              ScreenOrientation = ScreenOrientation.Portrait,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MyTableViewActivity : AppCompatActivity
    {
        ListView employeeListView;

        MyTableCellAdapter cellAdapter;

        IMenu _menuItem;

        public MainViewModel MainVm
        {
            get
            {
                return MainApplication.Locator.MainVm;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MyTableViewLayout);

            View customView = View.Inflate(this, Resource.Layout.Toolbar, null);
            var toolBar = customView.FindViewById<SupportToolbar>(Resource.Id.toolbar);
            TextView toolbarTitleTextView = customView.FindViewById<TextView>(Resource.Id.toolbar_title);
            TextView toolbarSubTitleTextView = customView.FindViewById<TextView>(Resource.Id.toolbar_sub_title);


            ImageView sort = FindViewById<ImageView>(Resource.Id.tileIcon);

            //sort.Click += (sender, e) =>
            //{
            //    sort.SetCommand(MainVm.SortActionCommand);
            //};

            sort.SetCommand("Click", MainVm.SortActionCommand);
            toolbarTitleTextView.Text = "Employee List";

            employeeListView = FindViewById<ListView>(Resource.Id.myListView);

            cellAdapter = new MyTableCellAdapter(this);

            employeeListView.Adapter = cellAdapter;

            employeeListView.ItemClick += EmployeeListView_ItemClick;

            SetSupportActionBar(toolBar);
            // ActionBar actionBar = getSupportActionBar();
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            MainVm.AllEmployees.CollectionChanged += AllEmployees_CollectionChanged;

        }
        void AllEmployees_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //TODO: perform any specific action (if any) that is needed after a collection is changed and then reload the data into list.

            employeeListView.Adapter = cellAdapter;
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();

            Finish();
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            _menuItem = menu;

            MenuInflater.Inflate(Resource.Menu.employee_list_menu, menu);

            _menuItem.GetItem(0).SetIcon(Resource.Drawable.add_participants);
            _menuItem.GetItem(1).SetIcon(Resource.Drawable.sort);

            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_sort_emp:
                    MainVm.SortActionCommand.Execute(null);
                    return true;
                case Resource.Id.action_add_new_emp:
                    // TODO: call method to add a new emp to list...
                    return true;
                default:
                    Finish();
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void EmployeeListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            StartActivity(typeof(DetailedActivity));

        }
		protected override void OnResume()
		{
            base.OnResume();
            //HockyApp Integration
            CrashManager.Register(this, "657027db39d245e6942bac733a0d14cf");
		}
	}
}