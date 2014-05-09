using System.Drawing;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views.PanelContainers
{
    /// <summary>
    /// Container class for Sliding Panels located on the left edge of the device screen
    /// </summary>
    public class DetailPanelContainer : PanelContainer
    {
        private readonly SplitDetailPanelContainer _parent;

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public DetailPanelContainer(SplitDetailPanelContainer parent, UIViewController panel)
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
                X = 0,//_parent.PanelPosition.X,
                Y = 0,//_parent.PanelPosition.Y,
                Width = _parent.View.Frame.Width,
                Height = _parent.View.Frame.Height
            };
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return new RectangleF
            {
                X = 0,//_parent.PanelPosition.X,
                Y = 0,//_parent.PanelPosition.Y,
                Width = _parent.View.Frame.Width,
                Height = _parent.View.Frame.Height
            };
        }

        #endregion

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            View.BackgroundColor = UIColor.Purple;
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
    }
}