using System.Drawing;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views
{
    [Register("SubMenuView")]
    public class SubMenuView : BaseViewController
    {
        public SubMenuView()
        {
            TypeOfView = ViewType.SubMenuView;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Cyan;

            var red = UIButton.FromType(UIButtonType.RoundedRect);
            red.Frame = new RectangleF(0, 10, 98, 40);
            red.Font = UIFont.FromName("Helvetica", 22);
            red.SetTitle("Red", UIControlState.Normal);
            Add(red);

            var modalButton = UIButton.FromType(UIButtonType.RoundedRect);
            modalButton.Frame = new RectangleF(0, 50, 98, 40);
            modalButton.Font = UIFont.FromName("Helvetica", 22);
            modalButton.SetTitle("Modal", UIControlState.Normal);
            Add(modalButton);

            var closeButton = UIButton.FromType(UIButtonType.RoundedRect);
            closeButton.Frame = new RectangleF(0, 90, 98, 40);
            closeButton.Font = UIFont.FromName("Helvetica", 22);
            closeButton.SetTitle("Close", UIControlState.Normal);
            Add(closeButton);

            var set = this.CreateBindingSet<SubMenuView, Core.ViewModels.SubMenuViewModel>();
            set.Bind(red).To(vm => vm.RedCommand);
            set.Bind(modalButton).To(vm => vm.ModalCommand);
            set.Bind(closeButton).To(vm => vm.CloseCommand);
            set.Apply();
        }
    }
}