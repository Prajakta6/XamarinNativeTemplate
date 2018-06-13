using System;
using CoreGraphics;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using XamarinNativeTemplate.iOS.ViewCell;
using XamarinNativeTemplate.iOS.Views;

namespace XamarinNativeTemplate.iOS
{
    public partial class MyTableViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
       // const string HomeViewCellIndentifier = "HomeViewCellIndentifier";
        const string ListViewCellIndentifier = "ListViewCell";

        MainViewModel MainVm
        {
            get
            {
                return AppDelegate.Locator.MainVm;
            }
        }

        //Binding listCountBinding;

        public MyTableViewController() : base("MyTableViewController", null)
        {
        }

        public override  void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Set the page title
            Title = StringsConstants.EMPLOYEE_LIST_PAGETITLE;

            // Set the navigation bar
            NavigationController.NavigationBar.Translucent = false;
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White
            };
            NavigationController.NavigationBar.TintColor = UIColor.White;
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 68, 131);
            NavigationController.NavigationBarHidden = false;

            NavigationController.NavigationBar.Layer.ShadowOpacity = 1.0f;
            NavigationController.NavigationBar.Layer.ShadowRadius = 2;
            NavigationController.NavigationBar.Layer.ShadowOffset = new CGSize(0, 1);
            NavigationController.NavigationBar.Layer.ShadowColor = UIColor.DarkGray.CGColor;

            var sortButtonImage = UIImage.FromFile("Sort.png");
            UIButton sortButtonItem = UIButton.FromType(UIButtonType.Custom);
            sortButtonItem.SetBackgroundImage(sortButtonImage, UIControlState.Normal);
            sortButtonItem.Frame = new CGRect(0, 0, 20, 20);

            sortButtonItem.SetCommand(MainVm.SortActionCommand);

            var sortButton = new UIBarButtonItem(sortButtonItem);
            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { sortButton };

            // await MainVm.DownloadAllEmployeesData();

            MainVm.BuildEmployeeList();

            tblHomeViewList.WeakDataSource = this;
            tblHomeViewList.WeakDelegate = this;

            tblHomeViewList.TableFooterView = new UIView(new CGRect(0, 0, 0, 0));

            MainVm.AllEmployees.CollectionChanged += AllEmployees_CollectionChanged;

            //listCountBinding = this.SetBinding(() => MainVm.AllEmployees.Count, () => "", BindingMode.OneWay);

        }

        void AllEmployees_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //TODO: perform any specific action (if any) that is needed after a collection is changed and then reload the data into list.

            tblHomeViewList.ReloadData();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        #region Data source methods of Table View

        [Export("tableView:numberOfRowsInSection:")]
        public nint RowsInSection(UITableView tableView, nint section)
        {
            if (MainVm.AllEmployees != null && MainVm.AllEmployees.Count > 0)
                return MainVm.AllEmployees.Count;
            
            return 0;
        }

        [Export("tableView:heightForRowAtIndexPath:")]
        public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 88;
        }

        [Export("tableView:cellForRowAtIndexPath:")]
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var listViewCell = (ListViewCell)tableView.DequeueReusableCell(ListViewCellIndentifier);

            if (listViewCell == null)
            {
                listViewCell = ListViewCell.Create();

                listViewCell.SelectionStyle = UITableViewCellSelectionStyle.None;
            }

            listViewCell.ConfigureHomeViewCell(indexPath.Row, MainVm.AllEmployees[indexPath.Row]);

            listViewCell.LayoutMargins = UIEdgeInsets.Zero;

            listViewCell.PreservesSuperviewLayoutMargins = false;

            return listViewCell;
        }

        #endregion

        #region Table View Delegate

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //var selectedItem = tableView.[indexPath.Row];// tableItems.ElementAt(e.Position);

           // var selectedItem = indexPath.Row;
            var SelectedItemData = MainVm.AllEmployees[indexPath.Row];             

            //TODO: action for row selected...
            NavigationController.PushViewController(new DetailedViewController(SelectedItemData), true);
            //DetailedViewController
        }

        #endregion
    }
}