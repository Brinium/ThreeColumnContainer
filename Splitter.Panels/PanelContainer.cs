using System.Drawing;
using MonoTouch.UIKit;

namespace Splitter.Panels
{
    public abstract class PanelContainer : UIViewController
    {
        protected readonly MasterPanelContainer _parent;

        /// <summary>
        /// Gets the panel position.
        /// </summary>
        /// <value>The panel position.</value>
        //public abstract RectangleF PanelPosition { get; set; }

        public static int ChildMargin
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the view controller contained inside this panel
        /// </summary>
        /// <value>The panel V.</value>
        public UIViewController PanelView
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a value indicating whether the panel is currently showing
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        public virtual bool IsVisible
        {
            get;
            set;
        }

        #region Construction / Destruction

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        protected PanelContainer(MasterPanelContainer parent, UIViewController view, RectangleF frame)
        {
            _parent = parent;
            PanelView = view;
            View.Frame = frame;
            IsVisible = true;
        }

        #endregion

        #region Panel Sizing

        protected RectangleF CreateViewPosition()
        {
            var orient = UIApplication.SharedApplication.StatusBarOrientation;
            switch (orient)
            {
                case UIInterfaceOrientation.LandscapeLeft:
                case UIInterfaceOrientation.LandscapeRight:
                    return HorizontalViewFrame();
                default:
                    return VerticalViewFrame();
            }
        }

        protected abstract RectangleF VerticalViewFrame();

        protected abstract RectangleF HorizontalViewFrame();

        private RectangleF CreateChildViewPosition()
        {
            return new RectangleF(new PointF(ChildMargin, ChildMargin), new SizeF(View.Frame.Width - ChildMargin * 2, View.Frame.Height - ChildMargin * 2));
        }

        #endregion

        #region View Lifecycle

        /// <summary>
        /// Called when the panel view is first loaded
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //View.Frame = CreateViewPosition();
            //PanelView.View.Frame = CreateChildViewPosition();

            AddChildViewController(PanelView);
            View.AddSubview(PanelView.View);
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            //View.Frame = CreateViewPosition();
            //PanelView.View.Frame = CreateViewPosition();
            PanelView.ViewWillAppear(animated);
            base.ViewWillAppear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidAppear(bool animated)
        {
            PanelView.ViewDidAppear(animated);
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Called whenever the Panel is about to be hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear(bool animated)
        {
            PanelView.ViewWillDisappear(animated);
            base.ViewWillDisappear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidDisappear(bool animated)
        {
            PanelView.ViewDidDisappear(animated);
            base.ViewDidDisappear(animated);
        }

        #endregion

        public virtual void SwapChildView(UIViewController newChildView, PanelType type)
        {
            if (newChildView == null)
                return;

            newChildView.View.Frame = CreateChildViewPosition();

            if (PanelView == null)
            {
                AddChildViewController(newChildView);
                View.AddSubview(newChildView.View);
            }
            else
            {
                AddChildViewController(newChildView);
                View.AddSubview(newChildView.View);
                newChildView.WillMoveToParentViewController(null);
                TransitionPanel(newChildView);
            }
        }

        public virtual void TransitionPanel(UIViewController newChildView)
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