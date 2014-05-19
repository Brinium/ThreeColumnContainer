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

        /// <summary>
        /// Gets the panel position.
        /// </summary>
        /// <value>The panel position.</value>
        public RectangleF PanelPosition
        {
            get
            {
                return new RectangleF
                {
                    X = View.Bounds.Width - Size.Width,
                    Y = -View.Frame.Y,
                    Width = Size.Width,
                    Height = View.Bounds.Height
                };
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }


        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        /// <param name="size">panel size</param>
        public SubMenuPanelContainer(MasterPanelContainer parent, UIViewController panel, SizeF size)
            : base(parent, panel, size)
        {
            _isVisible = true;
        }

        #endregion

        #region ViewLifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            PanelView.View.Frame = PanelPosition;
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            PanelView.View.Frame = PanelPosition;

            View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
            View.BackgroundColor = UIColor.Yellow;


            // Add a shadow to the top view so it looks like it is on top of the others
            View.Layer.ShadowOpacity = 0.75f;
            View.Layer.ShadowRadius = 10.0f;
            View.Layer.ShadowColor = UIColor.Black.CGColor;
            View.Layer.ShadowPath = UIBezierPath.FromRect(View.Layer.Bounds).CGPath;
        }

        #endregion
    }
}