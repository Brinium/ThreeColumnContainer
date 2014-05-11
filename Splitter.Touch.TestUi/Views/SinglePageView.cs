using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Splitter.Touch.Views;

namespace Splitter.Touch.TestUi.Views
{
    [Register("SinglePageView")]
    public class SinglePageView : SingleView
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.LightGray;

            var splitViewButton = UIButton.FromType(UIButtonType.RoundedRect);
            splitViewButton.Frame = new RectangleF(View.Frame.Width / 2 - 70, View.Frame.Height / 2 - 20, 140, 40);
            splitViewButton.Font = UIFont.FromName("Helvetica", 22);
            splitViewButton.SetTitle("Split View", UIControlState.Normal);
            Add(splitViewButton);

            var modalButton = UIButton.FromType(UIButtonType.RoundedRect);
            modalButton.Frame = new RectangleF(View.Frame.Width / 2 - 70, View.Frame.Height / 2 + 40, 140, 40);
            modalButton.Font = UIFont.FromName("Helvetica", 22);
            modalButton.SetTitle("Modal", UIControlState.Normal);
            Add(modalButton);

            var set = this.CreateBindingSet<SinglePageView, Core.ViewModels.SinglePageViewModel>();
            set.Bind(splitViewButton).To(vm => vm.NextCommand);
            set.Bind(modalButton).To(vm => vm.ModalCommand);
            set.Apply();
        }
    }
}