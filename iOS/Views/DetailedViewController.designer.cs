// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XamarinNativeTemplate.iOS.Views
{
	[Register ("DetailedViewController")]
	partial class DetailedViewController
	{
		[Outlet]
		UIKit.UIButton crashBtn { get; set; }

		[Outlet]
		UIKit.UILabel dobLbl { get; set; }

		[Outlet]
		UIKit.UILabel emailLbl { get; set; }

		[Outlet]
		UIKit.UILabel nameLbl { get; set; }

		[Outlet]
		UIKit.UILabel phoneLbl { get; set; }

		[Action ("crashBtnClick:")]
		partial void crashBtnClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (dobLbl != null) {
				dobLbl.Dispose ();
				dobLbl = null;
			}

			if (emailLbl != null) {
				emailLbl.Dispose ();
				emailLbl = null;
			}

			if (nameLbl != null) {
				nameLbl.Dispose ();
				nameLbl = null;
			}

			if (phoneLbl != null) {
				phoneLbl.Dispose ();
				phoneLbl = null;
			}

			if (crashBtn != null) {
				crashBtn.Dispose ();
				crashBtn = null;
			}
		}
	}
}
