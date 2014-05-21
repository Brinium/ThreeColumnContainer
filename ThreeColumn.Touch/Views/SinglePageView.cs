using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Splitter.Touch.Views
{
    [Register("SinglePageView")]
    public class SinglePageView : BaseViewController
    {
        public SinglePageView()
        {
            TypeOfView = ViewType.SingleView;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.LightGray;

            var splitViewButton = UIButton.FromType(UIButtonType.RoundedRect);
            splitViewButton.Frame = new RectangleF(View.Bounds.Width / 2 - 70, View.Bounds.Height / 2 - 20, 140, 40);
            splitViewButton.Font = UIFont.FromName("Helvetica", 22);
            splitViewButton.SetTitle("Split View", UIControlState.Normal);
            Add(splitViewButton);

            var modalButton = UIButton.FromType(UIButtonType.RoundedRect);
            modalButton.Frame = new RectangleF(View.Frame.Width / 2 - 70, View.Bounds.Height / 2 + 20, 140, 40);
            modalButton.Font = UIFont.FromName("Helvetica", 22);
            modalButton.SetTitle("Modal", UIControlState.Normal);
            Add(modalButton);

            var closeButton = UIButton.FromType(UIButtonType.RoundedRect);
            closeButton.Frame = new RectangleF(View.Bounds.Width / 2 - 70, View.Bounds.Height / 2 + 60, 140, 40);
            closeButton.Font = UIFont.FromName("Helvetica", 22);
            closeButton.SetTitle("Close", UIControlState.Normal);
            Add(closeButton);

            var set = this.CreateBindingSet<SinglePageView, Core.ViewModels.SinglePageViewModel>();
            set.Bind(splitViewButton).To(vm => vm.MenuCommand);
            set.Bind(modalButton).To(vm => vm.ModalCommand);
            set.Bind(closeButton).To(vm => vm.CloseCommand);
            set.Apply();
        }
    }
}