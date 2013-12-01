using System.Drawing;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views.PanelContainers
{
    /// <summary>
    /// Container class for Sliding Panels located on the left edge of the device screen
    /// </summary>
    public class DetailPanelContainer : PanelContainer
    {
        private readonly SplitPanelView _parent;
        protected override RectangleF PanelPosition
        {
            get
            {
                return new RectangleF
                {
                    X = _parent.PanelPosition.X + 100,
                    Y = _parent.PanelPosition.Y + 0,
                    Width = _parent.PanelPosition.Width - 100,
                    Height = _parent.PanelPosition.Height
                };
            }
        }

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public DetailPanelContainer(UIViewController panel, SplitPanelView parent)
            : base(panel)
        {
            _parent = parent;
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
            Transition(PanelView, newChildView, 1.0, UIViewAnimationOptions.CurveEaseOut, () => { },
                (finished) =>
                {
                    PanelView.RemoveFromParentViewController();
                    newChildView.DidMoveToParentViewController(this);
                    PanelView = newChildView;
                });
        }
    }
}