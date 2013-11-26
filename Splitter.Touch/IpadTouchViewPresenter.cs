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
    public class IpadTouchViewPresenter : MvxModalSupportTouchViewPresenter
    {
        /// <summary>
        /// the application's window.  We hang onto this so we can use it later
        /// </summary>
        private UIWindow _window;
        private SplitPanelView _splitPanelContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="IpadTouchViewPresenter"/> class.
        /// </summary>
        /// <param name="applicationDelegate">Application delegate.</param>
        /// <param name="window">Window.</param>
        public IpadTouchViewPresenter(UIApplicationDelegate applicationDelegate, UIWindow window) :
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
                        _splitPanelContainer = Mvx.Resolve<IMvxTouchViewCreator>().CreateView(new SplitPanelViewModel()) as SplitPanelView;
                        if (_splitPanelContainer != null)
                        {
                            _splitPanelContainer.MenuContainer = new MenuPanelContainer(viewController, _splitPanelContainer);
                            _splitPanelContainer.DetailContainer = new DetailPanelContainer(new EmptyView(UIColor.Green), _splitPanelContainer);
                            base.Show(_splitPanelContainer);
                        }
                        break;

                    case ViewType.SubMenuView:
                        if (_splitPanelContainer == null) return;
                        _splitPanelContainer.ChangePanelContents(new SubMenuPanelContainer(viewController, _splitPanelContainer), PanelType.SubMenuPanel);
                        break;

                    case ViewType.DetailView:
                        if (_splitPanelContainer == null) return;
                        _splitPanelContainer.ChangePanelContents(new DetailPanelContainer(viewController, _splitPanelContainer), PanelType.DetailPanel);
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