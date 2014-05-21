using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Splitter.Core.ViewModels
{
    public class SinglePageViewModel 
		: MvxViewModel
    {
        public ICommand  MenuCommand
        {
            get { return new MvxCommand(() => ShowViewModel<MenuViewModel>()); }
        }

        public ICommand ModalCommand
        {
            get { return new MvxCommand(() => ShowViewModel<ModalViewModel>()); }
        }

        public ICommand SingleCommand
        {
            get { return new MvxCommand(() => ShowViewModel<SinglePageViewModel>()); }
        }

        public ICommand CloseCommand
        {
            get { return new MvxCommand(() => Close(this)); }
        }
    }
}
