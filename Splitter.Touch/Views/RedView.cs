using System.Drawing;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views
{
    [Register("RedView")]
    public class RedView : BaseViewController
    {
        public RedView()
        {
            TypeOfView = ViewType.DetailView;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Red;

            var label = new UILabel();
            label.Text = "Red Label";
            label.Frame = new RectangleF(0, 0, 320, 50);
            label.Font = UIFont.FromName("Helvetica", 22);
            Add(label);
        }
    }
}