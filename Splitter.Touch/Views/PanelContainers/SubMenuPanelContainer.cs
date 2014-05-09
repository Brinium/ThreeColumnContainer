using System.Drawing;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views.PanelContainers
{
    /// <summary>
    /// Container class for Sliding Panels located on the left edge of the device screen
    /// </summary>
    public class SubMenuPanelContainer : PanelContainer
    {
        private readonly SplitDetailPanelContainer _parent;

        public static int Width { get { return 200; } }

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public SubMenuPanelContainer(SplitDetailPanelContainer parent, UIViewController panel)
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
                X = 5, //_parent.PanelPosition.X + Width,
                Y = 5, //_parent.PanelPosition.Y + 0,
                Width = Width - 10,
                Height = _parent.View.Frame.Height - 10
            };
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return new RectangleF
            {
                X = 5, //_parent.PanelPosition.X + Width,
                Y = 5, //_parent.PanelPosition.Y + 0,
                Width = Width - 10,
                Height = _parent.View.Frame.Height - 10
            };
        }

        #endregion

        #region ViewLifecycle

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            View.BackgroundColor = UIColor.Yellow;
        }

        public override void TransitionPanel(UIViewController newChildView)
        {
            PanelView.WillMoveToParentViewController(null);
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