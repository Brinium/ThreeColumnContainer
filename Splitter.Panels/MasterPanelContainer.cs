using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Panels
{
    public partial class MasterPanelContainer : UIViewController
    {
        public PanelContainer MenuContainer { get; set; }

        public SubMenuPanelContainer SubMenuContainer { get; set; }

        public DetailPanelContainer DetailContainer { get; set; }

        public float MenuMaxWidth { get; set; }

        public RectangleF CreateMenuFrame()
        {
            return new RectangleF
            {
                X = View.Frame.X,
                Y = View.Frame.Y,
                Width = MenuMaxWidth,
                Height = View.Frame.Height
            };
        }

        public float SubMenuMaxWidth { get; set; }

        public float SubMenuX()
        {
            return MenuContainer != null ? MenuContainer.View.Frame.X + MenuContainer.View.Frame.Width : 0;
        }

        public RectangleF CreateSubMenuFrame(bool isVisible)
        {
            return new RectangleF
            {
                X = SubMenuX(),
                Y = View.Frame.Y,
                Width = isVisible ? SubMenuMaxWidth : 0,
                Height = View.Frame.Height
            };
        }

        public RectangleF CreateDetailFrame()
        {
            var x = SubMenuContainer != null && SubMenuContainer.IsVisible ? SubMenuContainer.View.Frame.X + SubMenuContainer.View.Frame.Width : MenuContainer != null ? MenuContainer.View.Frame.X + MenuContainer.View.Frame.Width + 20 : 0;
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
            MenuMaxWidth = menuMaxWidth;
            SubMenuMaxWidth = subMenuMaxWidth;

            MenuContainer = new MenuPanelContainer(this, new EmptyView(UIColor.Yellow), CreateMenuFrame());

            SubMenuContainer = new SubMenuPanelContainer(this, new EmptyView(UIColor.Brown), CreateSubMenuFrame(true));
            //SubMenuContainer.IsVisible = false;

            DetailContainer = new DetailPanelContainer(this, new EmptyView(UIColor.Magenta), CreateDetailFrame());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitPanelView"/> class.
        /// </summary>
        /// <param name="menu">Menu</param>
        /// <param name="detail">Detail page</param>
        public MasterPanelContainer(float menuMaxWidth, float subMenuMaxWidth, MenuPanelContainer menu, SubMenuPanelContainer subMenu, DetailPanelContainer detail)
        {
            MenuMaxWidth = menuMaxWidth;
            SubMenuMaxWidth = subMenuMaxWidth;

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

            View.Frame = CreateViewPosition();

            if (MenuContainer != null)
            {
                AddChildViewController(MenuContainer);
                View.AddSubview(MenuContainer.View);
            }
            if (SubMenuContainer != null)
            {
                AddChildViewController(SubMenuContainer);
                View.AddSubview(SubMenuContainer.View);
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
                    newPanel = new MenuPanelContainer(this, newChildView, CreateMenuFrame());
                    break;
                case PanelType.SubMenuPanel:
                    newPanel = new SubMenuPanelContainer(this, newChildView, CreateSubMenuFrame(true));
                    break;
                case PanelType.DetailPanel:
                    newPanel = new DetailPanelContainer(this, newChildView, CreateDetailFrame());
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
            UIView.Animate(0.3f, () =>
            {
                var frame = SubMenuContainer.View.Frame;
                frame.X = SubMenuX();
                SubMenuContainer.View.Frame = frame;
            }, () =>
            {
            });
        }

        public void HideMenu()
        {
            UIView.Animate(0.3f, () =>
            {
                var frame = SubMenuContainer.View.Frame;
                frame.X = 0;
                SubMenuContainer.View.Frame = frame;
            }, () =>
            {
            });
        }

        private void ShowHideSubViewModel()
        {
            if (SubMenuContainer.IsVisible)
            {
                // Hide the panel and change the button's text
                var currLeftPanelRect = SubMenuContainer.View.Frame;
                currLeftPanelRect.X -= currLeftPanelRect.Size.Width / 2;
                var currRightPanelRect = DetailContainer.View.Frame;
                currRightPanelRect.X = 0;
                currRightPanelRect.Size.Width += currLeftPanelRect.Size.Width;
                // 1. Hide the panel
                UIView.Animate(0.5, () =>
                {
                    // b. Move left panel from (0, 0, w, h) to (-w, 0, w, h)
                    SubMenuContainer.View.Frame = currLeftPanelRect;
                    // c. Expand right panel from (x, 0, w, h) to (0, 0, w + x, h)
                    DetailContainer.View.Frame = currRightPanelRect;
                }, () =>
                {
                    SubMenuContainer.IsVisible = false;
                });
            }
            else
            {
                // Show the panel and change the button's text
                // 1. Show the panel
                UIView.Animate(0.5, () =>
                {
                    // b. Move left panel from (-w, 0, w, h) to (0, 0, w, h)
                    var currLeftPanelRect = SubMenuContainer.View.Frame;
                    currLeftPanelRect.X += currLeftPanelRect.Width / 2;
                    DetailContainer.View.Frame = currLeftPanelRect;
                    // c. Expand right panel from (0, 0, w, h) to (leftWidth, 0, w - leftWidth, h)
                    var currRightPanelRect = SubMenuContainer.View.Frame;
                    currRightPanelRect.X = currLeftPanelRect.Width;
                    currRightPanelRect.Width -= currLeftPanelRect.Width;
                    DetailContainer.View.Frame = currRightPanelRect;
                }, () =>
                {
                    SubMenuContainer.IsVisible = true;
                });
            }
        }

        #endregion
    }
}