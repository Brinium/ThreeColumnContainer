using System.Drawing;
using MonoTouch.UIKit;

namespace Splitter.Panels
{
    /// <summary>
    /// Container class for Sliding Panels located on the left edge of the device screen
    /// </summary>
    public class MenuPanelContainer : PanelContainer
    {
        public float Width { get; set; }

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public MenuPanelContainer(MasterPanelContainer parent, UIViewController panel, float width)
            : base(parent, panel)
        {
            Width = width;
        }

        #endregion

        #region Panel Sizing

        protected override RectangleF VerticalViewFrame()
        {
            return _parent.CreateMenuFrame();
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return _parent.CreateMenuFrame();
        }

        #endregion

        #region ViewLifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
            View.BackgroundColor = UIColor.Orange;
        }

        #endregion
    }
}