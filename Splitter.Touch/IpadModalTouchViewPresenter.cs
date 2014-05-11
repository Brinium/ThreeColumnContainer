using System;
using System.Linq;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using MonoTouch.UIKit;
using Splitter.Core.ViewModels;
using Splitter.Touch.Views;
using Splitter.Touch.Views.PanelContainers;

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
            var nav = MasterNavigationController;
            var top = nav.TopViewController;

            var type = toClose.GetType();
            var viewContainer = Mvx.Resolve<IMvxViewsContainer>();
            if (viewContainer == null) return;
            var view = viewContainer.GetViewType(type);

            if (view != null)
            {
                if (view == typeof (MenuView))
                {
                    
                }
                else if (view == typeof (SubMenuView))
                {
                    
                }
                else if (view == typeof (DetailView))
                {
                    
                }
                else if (view == typeof (SingleView))
                {
                    
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
                            masterView.MasterContainer = new MasterPanelContainer();
                            masterView.MasterContainer.MenuContainer = new MenuPanelContainer(masterView.MasterContainer, viewController);
                            masterView.MasterContainer.SplitContainer = new SplitDetailPanelContainer(masterView.MasterContainer);
                            masterView.MasterContainer.SplitContainer.DetailContainer = new DetailPanelContainer(masterView.MasterContainer.SplitContainer, new EmptyView(UIColor.Green));
                            this.Show(masterView);
                        }
                        break;

                    case ViewType.SubMenuView:
                        masterView = MasterNavigationController.TopViewController as MasterPanelView;
                        if (masterView == null)
                            return;
                        masterView.MasterContainer.ChangePanelContents(new SubMenuPanelContainer(masterView.MasterContainer.SplitContainer, viewController), PanelType.SubMenuPanel);
                        break;

                    case ViewType.DetailView:
                        masterView = MasterNavigationController.TopViewController as MasterPanelView;
                        if (masterView == null)
                            return;
                        masterView.MasterContainer.ChangePanelContents(new DetailPanelContainer(masterView.MasterContainer.SplitContainer, viewController), PanelType.DetailPanel);
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