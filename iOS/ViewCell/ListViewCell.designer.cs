// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XamarinNativeTemplate.iOS.ViewCell
{
	[Register ("ListViewCell")]
	partial class ListViewCell
	{
		[Outlet]
		UIKit.UILabel dobLbl { get; set; }

		[Outlet]
		UIKit.UILabel emailLbl { get; set; }

		[Outlet]
		UIKit.UILabel nameLbl { get; set; }

		[Outlet]
		UIKit.UILabel phoneLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (nameLbl != null) {
				nameLbl.Dispose ();
				nameLbl = null;
			}

			if (emailLbl != null) {
				emailLbl.Dispose ();
				emailLbl = null;
			}

			if (phoneLbl != null) {
				phoneLbl.Dispose ();
				phoneLbl = null;
			}

			if (dobLbl != null) {
				dobLbl.Dispose ();
				dobLbl = null;
			}
		}
	}
}
