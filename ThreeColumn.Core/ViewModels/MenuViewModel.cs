using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Splitter.Core.ViewModels
{
    public class MenuViewModel 
		: MvxViewModel
    {
        public ICommand BlueCommand
        {
            get { return new MvxCommand(() => ShowViewModel<BlueViewModel>());}
        }
        public ICommand RedCommand
        {
            get { return new MvxCommand(() => ShowViewModel<RedViewModel>()); }
        }
        public ICommand ModalCommand
        {
            get { return new MvxCommand(() => ShowViewModel<ModalViewModel>()); }
        }
        public ICommand SingleCommand
        {
            get { return new MvxCommand(() => ShowViewModel<SinglePageViewModel>()); }
        }
        public ICommand SubCommand
        {
            get { return new MvxCommand(() => ShowViewModel<SubMenuViewModel>()); }
        }
    }
}
