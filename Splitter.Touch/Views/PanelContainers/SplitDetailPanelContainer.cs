using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views.PanelContainers
{
    public class SplitDetailPanelContainer : PanelContainer
    {
        public PanelContainer SubMenuContainer { get; set; }

        public PanelContainer DetailContainer { get; set; }

        private readonly MasterPanelContainer _parent;

        #region Construction/Destruction

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        public SplitDetailPanelContainer(MasterPanelContainer parent)
			: base(new UIViewController())
        {
            _parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        /// <param name="detail">Detail page</param>
        /// <param name="parent">parent split panel</param>
        public SplitDetailPanelContainer(MasterPanelContainer parent, DetailPanelContainer detail)
			: base((new UIViewController()))
        {
            _parent = parent;
            DetailContainer = detail;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        /// <param name="detail">Detail page</param>
        /// <param name="parent">parent split panel</param>
        public SplitDetailPanelContainer(MasterPanelContainer parent, DetailPanelContainer detail, SubMenuPanelContainer subMenu)
            : base(new UIViewController())
        {
            _parent = parent;
            DetailContainer = detail;
            SubMenuContainer = subMenu;
        }

        #endregion

        #region Panel Sizing

        protected override RectangleF VerticalViewFrame()
        {
            return new RectangleF
            {
                X = _parent.View.Frame.X + MenuPanelContainer.Width + 5,
                Y = _parent.View.Frame.Y + 15,
                Width = _parent.View.Frame.Width - MenuPanelContainer.Width - 10,
                Height = _parent.View.Frame.Height - 20
            };
        }

        protected override RectangleF HorizontalViewFrame()
        {
            return new RectangleF
            {
                X = _parent.View.Frame.X + MenuPanelContainer.Width + 5,
                Y = _parent.View.Frame.Y + 15,
                Width = _parent.View.Frame.Width - MenuPanelContainer.Width - 10,
                Height = _parent.View.Frame.Height - 20
            };
        }

        #endregion

        #region ViewLifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Brown;

            if (DetailContainer != null)
            {
                AddChildViewController(DetailContainer);
                View.AddSubview(DetailContainer.View);
            }
            if (SubMenuContainer != null)
            {
                AddChildViewController(SubMenuContainer);
                View.AddSubview(SubMenuContainer.View);
            }
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            if (SubMenuContainer != null)
                SubMenuContainer.ViewWillAppear(animated);
            if (DetailContainer != null)
                DetailContainer.ViewWillAppear(animated);
            base.ViewWillAppear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidAppear(bool animated)
        {
            if (SubMenuContainer != null)
                SubMenuContainer.ViewDidAppear(animated);
            if (DetailContainer != null)
                DetailContainer.ViewDidAppear(animated);
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Called whenever the Panel is about to be hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear(bool animated)
        {
            if (SubMenuContainer != null)
                SubMenuContainer.ViewWillDisappear(animated);
            if (DetailContainer != null)
                DetailContainer.ViewWillDisappear(animated);
            base.ViewWillDisappear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidDisappear(bool animated)
        {
            if (SubMenuContainer != null)
                SubMenuContainer.ViewDidDisappear(animated);
            if (DetailContainer != null)
                DetailContainer.ViewDidDisappear(animated);
            base.ViewDidDisappear(animated);
        }

        #endregion

        #region overrides to pass to container

        /// <summary>
        /// Called when the view will rotate.
        /// This override forwards the WillRotate callback on to each of the panel containers
        /// </summary>
        /// <param name="toInterfaceOrientation">To interface orientation.</param>
        /// <param name="duration">Duration.</param>
        public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillRotate(toInterfaceOrientation, duration);
            if (SubMenuContainer != null)
                SubMenuContainer.WillRotate(toInterfaceOrientation, duration);
            if (DetailContainer != null)
                DetailContainer.WillRotate(toInterfaceOrientation, duration);
        }

        /// <summary>
        /// Called after the view rotated
        /// This override forwards the DidRotate callback on to each of the panel containers
        /// </summary>
        /// <param name="fromInterfaceOrientation">From interface orientation.</param>
        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);
            if (SubMenuContainer != null)
                SubMenuContainer.DidRotate(fromInterfaceOrientation);
            if (DetailContainer != null)
                DetailContainer.DidRotate(fromInterfaceOrientation);
        }

        #endregion

        #region Panel stuff

        /// <summary>
        /// Get the corresponding panel
        /// </summary>
        /// <param name="type">Type.</param>
        private PanelContainer GetPanel(PanelType type)
        {
            switch (type)
            {
                case PanelType.SubMenuPanel:
                    return SubMenuContainer;
                case PanelType.DetailPanel:
                    return DetailContainer;
                default:
                    return null;
            }
        }

        public void ChangePanelContents(UIViewController newChildView, PanelType type)
        {
            var activePanel = GetPanel(type);
            if (activePanel != null)
                activePanel.SwapChildView(newChildView);
            else
                DisplayNewChildView(newChildView, type);
        }

        protected void DisplayNewChildView(UIViewController newChildView, PanelType type)
        {
            PanelContainer newPanel = null;
            switch (type)
            {
                case PanelType.SubMenuPanel:
                    newPanel = new SubMenuPanelContainer(this, newChildView);
                    break;
                case PanelType.DetailPanel:
                    newPanel = new DetailPanelContainer(this, newChildView);
                    break;
            }

            if (newPanel == null)
                return;
            AddChildViewController(newPanel);
            View.AddSubview(newPanel.View);
            newPanel.DidMoveToParentViewController(null);
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