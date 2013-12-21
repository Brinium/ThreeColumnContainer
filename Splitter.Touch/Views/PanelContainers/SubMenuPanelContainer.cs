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
        private static int Width
        {
            get { return 200; }
        }
        public override RectangleF PanelPosition
        {
            get
            {
                return new RectangleF
                {
                    X = 0, //_parent.PanelPosition.X + Width,
                    Y = 0, //_parent.PanelPosition.Y + 0,
                    Width = Width,
                    Height = _parent.PanelPosition.Height
                };
            }
        }


        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="parent">parent split panel</param>
        public SubMenuPanelContainer(UIViewController panel, SplitDetailPanelContainer parent)
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
            View.BackgroundColor = UIColor.Yellow;
        }

        public override void TransitionPanel(UIViewController newChildView)
        {
            PanelView.WillMoveToParentViewController(null);
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