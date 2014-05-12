using System.Drawing;
using MonoTouch.UIKit;

namespace Splitter.Panels
{
    /// <summary>
    /// Container class for Sliding Panels located on the left edge of the device screen
    /// </summary>
    public class MenuPanelContainer : PanelContainer
    {
        private readonly MasterPanelContainer _parent;

        public static int Width { get { return 150; } }

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public MenuPanelContainer(MasterPanelContainer parent, UIViewController panel)
            : base(panel)
        {
            _parent = parent;
        }

        #endregion

        #region Panel Sizing

        protected override RectangleF VerticalViewFrame()
        {
            return new RectangleF
            {
                X = _parent.View.Frame.X + 5,
                Y = _parent.View.Frame.Y + 5,
                Width = Width - 10,
                Height = _parent.View.Frame.Height - 10
            };
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return new RectangleF
            {
                X = _parent.View.Frame.X + 5,
                Y = _parent.View.Frame.Y + 5,
                Width = Width - 10,
                Height = _parent.View.Frame.Height - 10
            };
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
            View.BackgroundColor = UIColor.Orange;
        }

        public override void TransitionPanel(UIViewController newChildView)
        {
            Transition(PanelView, newChildView, 1.0, UIViewAnimationOptions.CurveEaseOut, () =>
            {
            },
                (finished) =>
                {
                    PanelView.RemoveFromParentViewController();
                    newChildView.DidMoveToParentViewController(this);
                    PanelView = newChildView;
                });
        }

        #endregion
    }
}