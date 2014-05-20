using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Panels
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