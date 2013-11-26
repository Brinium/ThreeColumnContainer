using System.Drawing;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views.PanelContainers
{
    [Register("EmptyView")]
    public class EmptyView : UIViewController
    {
        public UIColor Background { get; set; }

        public EmptyView(UIColor background)
        {
            Background = background;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = Background;
        }
    }
}