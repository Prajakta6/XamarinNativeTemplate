using System;

using Foundation;
using UIKit;

namespace XamarinNativeTemplate.iOS
{
    public partial class HomeListViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("HomeListViewCell");
        public static readonly UINib Nib;

        static HomeListViewCell()
        {
            Nib = UINib.FromName("HomeListViewCell", NSBundle.MainBundle);
        }

        protected HomeListViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public static HomeListViewCell Create()
        {
            var cell = (HomeListViewCell)Nib.Instantiate(null, null)[0];

            return cell;
        }

        public void ConfigureHomeViewCell(int pintJobItemIndex, Employee pEmp)
        {
            //TODO: confirgure the cell data here...

        }
    }
}
