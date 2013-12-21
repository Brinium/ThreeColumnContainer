using System;
using System.Collections.Generic;
using System.Drawing;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views.PanelContainers
{
    public partial class SplitPanelView : MvxViewController
    {
        private bool _firstTime = true;
        public PanelContainer MenuContainer { get; set; }

        public SplitDetailPanelContainer SplitContainer { get; set; }

        public RectangleF PanelPosition
        {
            get
            {
                var height = NavigationController == null ? 44 : NavigationController.NavigationBar.Frame.Height;
                return new RectangleF(new PointF(0, UIScreen.MainScreen.ApplicationFrame.Y + height), new SizeF(UIScreen.MainScreen.ApplicationFrame.Width, UIScreen.MainScreen.ApplicationFrame.Height - height));
            }
        }

        #region Construction/Destruction
        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        public SplitPanelView()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        /// <param name="menu">Menu</param>
        /// <param name="detail">Detail page</param>
        public SplitPanelView(MenuPanelContainer menu, DetailPanelContainer detail)
        {
            //MenuContainer = new MenuPanelContainer(new EmptyView(UIColor.Yellow));
            MenuContainer = menu;
            //DetailContainer = new DetailPanelContainer(new EmptyView(UIColor.Magenta));
            SplitContainer = new SplitDetailPanelContainer(detail, this);
        }
        #endregion

        #region ViewLifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.Frame = PanelPosition;

            AddChildViewController(MenuContainer);
            View.AddSubview(MenuContainer.View);
            AddChildViewController(SplitContainer);
            View.AddSubview(SplitContainer.View);
        }

        /// <summary>
        /// At this point, it is safe to assume that the Superview is available
        /// for us to insert any panel that may have been added already.
        /// <see cref="_firstTime"/> 
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        //        public override void ViewDidAppear(bool animated)
        //        {
        //            base.ViewDidAppear(animated);
        //
        //            if (_firstTime)
        //            {
        //                View.Superview.AddSubview(MenuContainer.View);
        //                View.Superview.AddSubview(DetailContainer.View);
        //
        //                UIView parent = View.Superview;
        //                View.RemoveFromSuperview();
        //                parent.AddSubview(View);
        //
        //                _firstTime = false;
        //            }
        //        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            if (MenuContainer != null)
                MenuContainer.ViewWillAppear(animated);
            if (SplitContainer != null)
                SplitContainer.ViewWillAppear(animated); ;
            base.ViewWillAppear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidAppear(bool animated)
        {
            if (MenuContainer != null)
                MenuContainer.ViewDidAppear(animated);
            if (SplitContainer != null)
                SplitContainer.ViewDidAppear(animated);
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Called whenever the Panel is about to be hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear(bool animated)
        {
            if (MenuContainer != null)
                MenuContainer.ViewWillDisappear(animated);
            if (SplitContainer != null)
                SplitContainer.ViewWillDisappear(animated);
            base.ViewWillDisappear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidDisappear(bool animated)
        {
            if (MenuContainer != null)
                MenuContainer.ViewDidDisappear(animated);
            if (SplitContainer != null)
                SplitContainer.ViewDidDisappear(animated);
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
            if (MenuContainer != null)
                MenuContainer.WillRotate(toInterfaceOrientation, duration);
            if (SplitContainer != null)
                SplitContainer.WillRotate(toInterfaceOrientation, duration);
        }

        /// <summary>
        /// Called after the view rotated
        /// This override forwards the DidRotate callback on to each of the panel containers
        /// </summary>
        /// <param name="fromInterfaceOrientation">From interface orientation.</param>
        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);
            if (MenuContainer != null)
                MenuContainer.DidRotate(fromInterfaceOrientation);
            if (SplitContainer != null)
                SplitContainer.DidRotate(fromInterfaceOrientation);
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
                case PanelType.MenuPanel:
                    return MenuContainer;
                case PanelType.SubMenuPanel:
                    return SplitContainer.SubMenuContainer;
                case PanelType.DetailPanel:
                    return SplitContainer.DetailContainer;
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
                case PanelType.MenuPanel:
                    newPanel = new MenuPanelContainer(newChildView, this);
                    break;
                case PanelType.SubMenuPanel:
                    if (SplitContainer == null)
                    {
                        newPanel = new SplitDetailPanelContainer(this);
                        ((SplitDetailPanelContainer)newPanel).SubMenuContainer = new SubMenuPanelContainer(newChildView, ((SplitDetailPanelContainer)newPanel));
                    }
                    else
                    {
                        SplitContainer.ChangePanelContents(newChildView, type);
                    }
                    break;
                case PanelType.DetailPanel:
                    if (SplitContainer == null)
                    {
                        newPanel = new SplitDetailPanelContainer(this);
                        ((SplitDetailPanelContainer)newPanel).DetailContainer = new DetailPanelContainer(newChildView, ((SplitDetailPanelContainer)newPanel));
                    }
                    else
                    {
                        SplitContainer.ChangePanelContents(newChildView, type);
                    }
                    break;
            }

            if (newPanel == null) return;
            AddChildViewController(newPanel);
            View.AddSubview(newPanel.View);
            newPanel.DidMoveToParentViewController(null);
        }
        #endregion
    }
}