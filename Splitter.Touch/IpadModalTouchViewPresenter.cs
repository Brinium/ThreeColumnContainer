using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.UIKit;
using Splitter.Core.ViewModels;
using Splitter.Panels;
using Splitter.Touch.Views;
using Cirrious.MvvmCross.Views;

namespace Splitter.Touch
{
    /// <summary>
    /// View Presenter for this application
    /// Manages automatically creating the Sliding panel view controller and its 
    /// requisite panel views as well as showing normal views
    /// </summary>
    public class IpadModalTouchViewPresenter : MvxModalSupportTouchViewPresenter
    {
        /// <summary>
        /// the application's window.  We hang onto this so we can use it later
        /// </summary>
        private UIWindow _window;
        //private SplitPanelView _splitPanelContainer;
        /// <summary>
        /// Initializes a new instance of the <see cref="IpadModalTouchViewPresenter"/> class.
        /// </summary>
        /// <param name="applicationDelegate">Application delegate.</param>
        /// <param name="window">Window.</param>
        public IpadModalTouchViewPresenter(UIApplicationDelegate applicationDelegate, UIWindow window) :
            base(applicationDelegate, window)
        {
            // specialized construction logic goes here
            _window = window;
        }

        public override void Close(IMvxViewModel toClose)
        {
            var root = _window.RootViewController;

            var view = Mvx.Resolve<IMvxTouchViewCreator>().CreateView(toClose) as BaseViewController;

            var masterView = MasterNavigationController.TopViewController as MasterPanelView;
            if (masterView != null && view != null)
            {
                switch (view.TypeOfView)
                {
                    case ViewType.MenuView:
                        //close masterView or nav back
                        break;
                    case ViewType.SubMenuView:
                        //masterView.ClosePanel(PanelType.SubMenuPanel);
                        break;
                    case ViewType.DetailView:
                        //masterView.ClosePanel(PanelType.SubMenuPanel);
                        break;
                    case ViewType.SingleView:
                        base.Close(toClose);
                        break;
                }
            }

            base.Close(toClose);
        }

        public override void Show(IMvxTouchView view)
        {
            var viewController = view as BaseViewController;

            if (viewController != null)
            {
                switch (viewController.TypeOfView)
                {
                    case ViewType.MenuView:
                        var masterView = Mvx.Resolve<IMvxTouchViewCreator>().CreateView(new MasterPanelViewModel()) as MasterPanelView;
                        if (masterView != null)
                        {
                            masterView.MasterContainer = new MasterPanelContainer(150, 200);
                            masterView.MasterContainer.ChangePanelContents(viewController, PanelType.MenuPanel);
                            masterView.MasterContainer.ChangePanelContents(new EmptyView(UIColor.Green), PanelType.DetailPanel);
                            this.Show(masterView);
                        }
                        break;

                    case ViewType.SubMenuView:
                        masterView = MasterNavigationController.TopViewController as MasterPanelView;
                        if (masterView == null)
                            return;
                        masterView.MasterContainer.ChangePanelContents(viewController, PanelType.SubMenuPanel);
                        masterView.MasterContainer.ShowSubMenu();
                        break;

                    case ViewType.DetailView:
                        masterView = MasterNavigationController.TopViewController as MasterPanelView;
                        if (masterView == null)
                            return;
                        masterView.MasterContainer.ChangePanelContents(viewController, PanelType.DetailPanel);
                        break;

                    case ViewType.SingleView:
                        base.Show(view);
                        break;
                }
            }
            else
                base.Show(view);
        }
    }
}