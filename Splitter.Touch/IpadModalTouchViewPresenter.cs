using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
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

        public override void Show(IMvxTouchView view)
        {
            var viewController = view as BaseViewController;

            if (viewController != null)
            {
                switch (viewController.TypeOfView)
                {
                    case ViewType.MenuView:
                        var splitContainer = Mvx.Resolve<IMvxTouchViewCreator>().CreateView(new SplitPanelViewModel()) as SplitPanelView;
                        if (splitContainer != null)
                        {
                            splitContainer.MenuContainer = new MenuPanelContainer(viewController, splitContainer);
                            splitContainer.SplitContainer = new SplitDetailPanelContainer(splitContainer);
                            splitContainer.SplitContainer.DetailContainer = new DetailPanelContainer(new EmptyView(UIColor.Green), splitContainer.SplitContainer);
                            this.Show(splitContainer);
                        }
                        break;

                    case ViewType.SubMenuView:
                        splitContainer = MasterNavigationController.TopViewController as SplitPanelView;
                        if (splitContainer == null) return;
                        splitContainer.ChangePanelContents(new SubMenuPanelContainer(viewController, splitContainer.SplitContainer), PanelType.SubMenuPanel);
                        break;

                    case ViewType.DetailView:
                        splitContainer = MasterNavigationController.TopViewController as SplitPanelView;
                        if (splitContainer == null) return;
                        splitContainer.ChangePanelContents(new DetailPanelContainer(viewController, splitContainer.SplitContainer), PanelType.DetailPanel);
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