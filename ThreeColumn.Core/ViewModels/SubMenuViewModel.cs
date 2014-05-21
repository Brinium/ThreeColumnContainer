using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Splitter.Core.ViewModels
{
    public class SubMenuViewModel 
		: MvxViewModel
    {
        public ICommand RedCommand
        {
            get { return new MvxCommand(() => ShowViewModel<RedViewModel>()); }
        }

        public ICommand ModalCommand
        {
            get { return new MvxCommand(() => ShowViewModel<ModalViewModel>()); }
        }

        public ICommand CloseCommand
        {
            get { return new MvxCommand(() => Close(this)); }
        }
    }
}
