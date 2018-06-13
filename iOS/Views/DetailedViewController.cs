using System;

using UIKit;

namespace XamarinNativeTemplate.iOS.Views
{
    public partial class DetailedViewController : UIViewController
    {
       


        Employee employeeData;

        public DetailedViewController(Employee emp) : base("DetailedViewController", null)
        {
            employeeData = emp;
        }

        public override void ViewDidLoad()
        { 
            // Perform any additional setup after loading the view, typically from a nib.
            base.ViewDidLoad();

            if (employeeData != null)
            {
                nameLbl.Text = employeeData.Name;
                phoneLbl.Text = employeeData.Phone;
                emailLbl.Text = employeeData.EmailId;
                dobLbl.Text = employeeData.DOB;
            }
            crashBtn.TouchUpInside += CrashBtn_TouchUpInside;
           
        }
        public void CrashBtn_TouchUpInside(object sender, EventArgs e)
        {
            nameLbl = null;
            nameLbl.Text = "hi";
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

