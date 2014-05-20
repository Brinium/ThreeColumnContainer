using System.Drawing;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views
{
	[Register ("BlueView")]
	public class BlueView : BaseViewController
	{
		public BlueView ()
		{
			TypeOfView = ViewType.DetailView;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.Blue;

			var label = new UILabel ();
			label.Text = "Blue Label";
			label.Frame = new RectangleF (20, 20, 320, 50);
			label.Font = UIFont.FromName ("Helvetica", 22);
			Add (label);
		}
	}
}