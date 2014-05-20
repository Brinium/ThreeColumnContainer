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
        public float Width { get; set; }

        /// <summary>
        /// Gets a value indicating whether the panel is currently showing
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        public virtual bool IsVisible
        {
            get;
            set;
        }

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public SubMenuPanelContainer(MasterPanelContainer parent, UIViewController panel, float width)
            : base(parent, panel)
        {
            Width = width;
        }

        #endregion

        #region Panel Sizing

        protected override RectangleF VerticalViewFrame()
        {
            return _parent.CreateSubMenuFrame();
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return _parent.CreateSubMenuFrame();
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
            View.BackgroundColor = UIColor.Yellow;


            // Add a shadow to the top view so it looks like it is on top of the others
//            View.Layer.ShadowOpacity = 0.75f;
//            View.Layer.ShadowRadius = 10.0f;
//            View.Layer.ShadowColor = UIColor.Black.CGColor;
//            View.Layer.ShadowPath = UIBezierPath.FromRect(View.Layer.Bounds).CGPath;
        }

        #endregion
    }
}