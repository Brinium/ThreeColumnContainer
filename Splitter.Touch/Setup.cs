using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Platform;

namespace Splitter.Touch
{
	public class Setup : MvxTouchSetup
	{
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)//, UIWindow window)
            : base(applicationDelegate, presenter)//, window)
		{
		}

		protected override Cirrious.MvvmCross.ViewModels.IMvxApplication CreateApp ()
		{
			return new Core.App();
		}

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
	}
}