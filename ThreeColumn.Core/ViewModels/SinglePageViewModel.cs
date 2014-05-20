using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Splitter.Core.ViewModels
{
    public class SinglePageViewModel 
		: MvxViewModel
    {
        public ICommand NextCommand
        {
            get { return new MvxCommand(() => ShowViewModel<MenuViewModel>());}
        }
        public ICommand ModalCommand
        {
            get { return new MvxCommand(() => ShowViewModel<ModalViewModel>()); }
        }
    }
}
