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
	[Register ("HomeListViewCell")]
	partial class HomeListViewCell
	{
		[Outlet]
		UIKit.UILabel lblCellViewText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblCellViewText != null) {
				lblCellViewText.Dispose ();
				lblCellViewText = null;
			}
		}
	}
}
