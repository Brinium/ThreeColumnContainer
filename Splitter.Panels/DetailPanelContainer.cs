using System.Drawing;
using MonoTouch.UIKit;

namespace Splitter.Panels
{
    /// <summary>
    /// Container class for Sliding Panels located on the left edge of the device screen
    /// </summary>
    public class DetailPanelContainer : PanelContainer
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public DetailPanelContainer(MasterPanelContainer parent, UIViewController panel, RectangleF frame)
            : base(parent, panel, frame)
        {
        }

        #endregion

        #region Panel Sizing

        protected override RectangleF VerticalViewFrame()
        {
            return View.Frame;//_parent.DetailFrame;
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return View.Frame;//_parent.DetailFrame;
        }

        #endregion

        #region ViewLifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            View.BackgroundColor = UIColor.Purple;
        }

        #endregion
    }
}