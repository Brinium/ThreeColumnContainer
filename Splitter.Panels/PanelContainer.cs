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
        /// Gets the size of the panel
        /// </summary>
        /// <value>The size.</value>
        public virtual SizeF Size
        {
            get;
            private set;
        }

        #region Construction / Destruction

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        protected PanelContainer(MasterPanelContainer parent, UIViewController view, SizeF size)
        {
            _parent = parent;
            PanelView = view;
            Size = size;
        }

        #endregion

        #region View Lifecycle

        /// <summary>
        /// Called when the panel view is first loaded
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.Frame = UIScreen.MainScreen.ApplicationFrame;

            AddChildViewController(PanelView);
            View.AddSubview(PanelView.View);
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            var frame = UIScreen.MainScreen.ApplicationFrame;

            if (InterfaceOrientation != UIInterfaceOrientation.Portrait)
            {
                frame.Width = UIScreen.MainScreen.ApplicationFrame.Height;
                frame.Height = UIScreen.MainScreen.ApplicationFrame.Width;
                frame.X = UIScreen.MainScreen.ApplicationFrame.Y;

                if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft)
                {
                    frame.Y = UIScreen.MainScreen.ApplicationFrame.X;
                }
                else
                {
                    frame.Y = UIScreen.MainScreen.Bounds.Width - UIScreen.MainScreen.ApplicationFrame.Width;
                }

            }

            View.Frame = frame;

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