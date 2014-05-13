using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace Splitter.Panels
{
    /// <summary>
    /// Container class for Sliding Panels located on the left edge of the device screen
    /// </summary>
    public class SubMenuPanelContainer : PanelContainer
    {
        private readonly MasterPanelContainer _parent;

        public static int Width { get { return 200; } }

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public SubMenuPanelContainer(MasterPanelContainer parent, UIViewController panel)
            : base(panel)
        {
            _parent = parent;
        }

        #endregion

        #region Panel Sizing

        protected override RectangleF VerticalViewFrame()
        {
            return  _parent.SubMenuFrame;
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return _parent.SubMenuFrame;
        }

        #endregion

        #region ViewLifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            View.BackgroundColor = UIColor.Yellow;


            // Add a shadow to the top view so it looks like it is on top of the others
            View.Layer.ShadowOpacity = 0.75f;
            View.Layer.ShadowRadius = 10.0f;
            View.Layer.ShadowColor = UIColor.Black.CGColor;
        }

        #endregion
    }
}