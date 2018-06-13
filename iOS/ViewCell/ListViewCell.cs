using System;

using Foundation;
using UIKit;

namespace XamarinNativeTemplate.iOS.ViewCell
{
    public partial class ListViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ListViewCell");
        public static readonly UINib Nib;

        static ListViewCell()
        {
            Nib = UINib.FromName("ListViewCell", NSBundle.MainBundle);
        }

        protected ListViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public static ListViewCell Create()
        {
            var cell = (ListViewCell)Nib.Instantiate(null, null)[0];

            return cell;
        }

        public void ConfigureHomeViewCell(int pintJobItemIndex, Employee pEmp)
        {
            if (pEmp != null)
            {
                nameLbl.Text = pEmp.Name;
                emailLbl.Text = pEmp.EmailId;
                phoneLbl.Text = pEmp.Phone;
                dobLbl.Text = pEmp.DOB;
            }
        }
    }
}
