using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Splitter.Core.ViewModels
{
    public class ModalViewModel
        : MvxViewModel
    {

        private MvxCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                _closeCommand = _closeCommand ?? new MvxCommand(OnClose);
                return _closeCommand;
            }
        }

        private void OnClose()
        {
            Close(this);
        }
    }
}