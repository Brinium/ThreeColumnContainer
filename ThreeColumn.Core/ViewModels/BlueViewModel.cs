using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;

namespace Splitter.Core.ViewModels
{
    public class BlueViewModel : MvxViewModel
    {
        public ICommand MenuCommand
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