using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Panels
{
    public partial class MasterPanelContainer : UIViewController
    {
        public MenuPanelContainer MenuContainer { get; set; }

        public SubMenuPanelContainer SubMenuContainer { get; set; }

        public DetailPanelContainer DetailContainer { get; set; }

        public RectangleF CreateMenuFrame()
        {
            return new RectangleF
            {
                X = View.Frame.X,
                Y = View.Frame.Y,
                Width = MenuContainer.Width,
                Height = View.Frame.Height
            };
        }

        public float SubMenuX()
        {
            return MenuContainer != null ? MenuContainer.View.Frame.X + MenuContainer.View.Frame.Width : 0;
        }

        public RectangleF CreateSubMenuFrame()
        {
            return new RectangleF
            {
                X = SubMenuX(),
                Y = View.Frame.Y,
                Width = SubMenuContainer.IsVisible ? SubMenuContainer.Width : 0,
                Height = View.Frame.Height
            };
        }

        public RectangleF CreateDetailFrame()
        {
            var x = SubMenuContainer != null && SubMenuContainer.IsVisible ? SubMenuContainer.View.Frame.X + SubMenuContainer.View.Frame.Width : MenuContainer != null ? MenuContainer.View.Frame.X + MenuContainer.View.Frame.Width : 0;
            return new RectangleF
            {
                X = x,
                Y = View.Frame.Y,
                Width = View.Frame.Width - x,
                Height = View.Frame.Height
            };
        }

        #region Construction/Destruction

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        public MasterPanelContainer(float menuMaxWidth, float subMenuMaxWidth)
        {
            MenuContainer = new MenuPanelContainer(this, new EmptyView(UIColor.Yellow), menuMaxWidth);

            SubMenuContainer = new SubMenuPanelContainer(this, new EmptyView(UIColor.Brown), subMenuMaxWidth);
            SubMenuContainer.IsVisible = false;

            DetailContainer = new DetailPanelContainer(this, new EmptyView(UIColor.Magenta), menuMaxWidth + subMenuMaxWidth);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        /// <param name="menu">Menu</param>
        /// <param name="detail">Detail page</param>
        public MasterPanelContainer(float menuMaxWidth, float subMenuMaxWidth, MenuPanelContainer menu, SubMenuPanelContainer subMenu, DetailPanelContainer detail)
        {
            MenuContainer = menu;
            SubMenuContainer = subMenu;
            //SubMenuContainer.IsVisible = false;
            DetailContainer = detail;
        }

        #endregion

        #region Panel Sizing

        private RectangleF CreateViewPosition()
        {
            var orient = UIApplication.SharedApplication.StatusBarOrientation;
            switch (orient)
            {
                case UIInterfaceOrientation.LandscapeLeft:
                case UIInterfaceOrientation.LandscapeRight:
                    return HorizontalViewPosition();
                default:
                    return VerticalViewPosition();
            }
        }

        private RectangleF VerticalViewPosition()
        {
            var bounds = UIScreen.MainScreen.Bounds;
            var navHeight = NavigationController.NavigationBarHidden ? 0 : NavigationController.NavigationBar.Frame.Height;
            var pointX = UIScreen.MainScreen.ApplicationFrame.X;
            var pointY = navHeight - 12;//UIScreen.MainScreen.ApplicationFrame.Y * 2;// + navHeight + UIApplication.SharedApplication.StatusBarFrame.Height;
            var width = UIScreen.MainScreen.ApplicationFrame.Width;
            var height = UIScreen.MainScreen.ApplicationFrame.Height - navHeight;// - UIApplication.SharedApplication.StatusBarFrame.Height;
            return new RectangleF(new PointF(pointX, pointY), new SizeF(width, height));
        }

        private RectangleF HorizontalViewPosition()
        {
            var bounds = UIScreen.MainScreen.Bounds;
            var navHeight = NavigationController.NavigationBarHidden ? 0 : NavigationController.NavigationBar.Frame.Height;
            var pointX = UIScreen.MainScreen.ApplicationFrame.Y;
            var pointY = navHeight - 12;// - UIScreen.MainScreen.ApplicationFrame.X;// + navHeight;// + UIApplication.SharedApplication.StatusBarFrame.Width;
            var width = UIScreen.MainScreen.ApplicationFrame.Width + UIScreen.MainScreen.ApplicationFrame.X;
            var height = UIScreen.MainScreen.ApplicationFrame.Height - navHeight - UIApplication.SharedApplication.StatusBarFrame.Width;
            return new RectangleF(new PointF(pointX, pointY), new SizeF(width, height));
        }

        #endregion

        #region ViewLifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.Frame = UIScreen.MainScreen.ApplicationFrame;

            if (SubMenuContainer != null)
            {
                AddChildViewController(SubMenuContainer);
                View.AddSubview(SubMenuContainer.View);
            }
            if (MenuContainer != null)
            {
                AddChildViewController(MenuContainer);
                View.AddSubview(MenuContainer.View);
            }
            if (DetailContainer != null)
            {
                AddChildViewController(DetailContainer);
                View.AddSubview(DetailContainer.View);
            }
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            View.Frame = CreateViewPosition();

            if (MenuContainer != null)
                MenuContainer.ViewWillAppear(animated);
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
            if (MenuContainer != null)
                MenuContainer.ViewDidAppear(animated);
            if (SubMenuContainer != null)
                SubMenuContainer.ViewWillAppear(animated);
            if (DetailContainer != null)
                DetailContainer.ViewWillAppear(animated);
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
            if (SubMenuContainer != null)
                SubMenuContainer.ViewWillAppear(animated);
            if (DetailContainer != null)
                DetailContainer.ViewWillAppear(animated);
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
            if (SubMenuContainer != null)
                SubMenuContainer.ViewWillAppear(animated);
            if (DetailContainer != null)
                DetailContainer.ViewWillAppear(animated);
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
            if (MenuContainer != null)
                MenuContainer.DidRotate(fromInterfaceOrientation);
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
                case PanelType.MenuPanel:
                    return MenuContainer;
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
                activePanel.SwapChildView(newChildView, type);
            else
                DisplayNewChildView(newChildView, type);
        }

        protected void DisplayNewChildView(UIViewController newChildView, PanelType type)
        {
            PanelContainer newPanel = null;
            switch (type)
            {
                case PanelType.MenuPanel:
                    newPanel = new MenuPanelContainer(this, newChildView, 150);
                    break;
                case PanelType.SubMenuPanel:
                    newPanel = new SubMenuPanelContainer(this, newChildView, 200);
                    break;
                case PanelType.DetailPanel:
                    newPanel = new DetailPanelContainer(this, newChildView, 350);
                    break;
            }

            if (newPanel == null)
                return;
            AddChildViewController(newPanel);
            View.AddSubview(newPanel.View);
            newPanel.DidMoveToParentViewController(null);
        }

        public void ShowSubMenu()
        {
            SubMenuContainer.IsVisible = true;
            UIView.Animate(0.3f, () =>
            {
                SubMenuContainer.View.Frame = CreateSubMenuFrame();
                DetailContainer.View.Frame = CreateDetailFrame();
            }, () =>
            {
            });
        }

        public void HideMenu()
        {
            SubMenuContainer.IsVisible = false;
            UIView.Animate(0.3f, () =>
            {
                SubMenuContainer.View.Frame = CreateSubMenuFrame();
                DetailContainer.View.Frame = CreateDetailFrame();
            }, () =>
            {
            });
        }

        #endregion
    }
}