// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XamarinNativeTemplate.iOS
{
	[Register ("MyTableViewController")]
	partial class MyTableViewController
	{
		[Outlet]
		UIKit.UITableView tblHomeViewList { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblHomeViewList != null) {
				tblHomeViewList.Dispose ();
				tblHomeViewList = null;
			}
		}
	}
}