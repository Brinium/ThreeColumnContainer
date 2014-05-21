using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Splitter.Touch.Views
{
    [Register("RedView")]
    public class RedView : BaseViewController
    {
        UIButton modalButton { get; set; }

        UIButton splitViewButton { get; set; }

        UIButton single { get; set; }

        UIButton closeButton{ get; set; }

        public RedView()
        {
            TypeOfView = ViewType.DetailView;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Red;

            var label = new UILabel();
            label.Text = "Red Label";
            label.Frame = new RectangleF(20, 20, 320, 50);
            label.Font = UIFont.FromName("Helvetica", 22);
            Add(label);

            splitViewButton = UIButton.FromType(UIButtonType.RoundedRect);
            splitViewButton.Frame = new RectangleF(View.Bounds.Width / 2 - 70, View.Bounds.Height / 2 - 20, 140, 40);
            splitViewButton.Font = UIFont.FromName("Helvetica", 22);
            splitViewButton.SetTitle("Split View", UIControlState.Normal);
            Add(splitViewButton);

            modalButton = UIButton.FromType(UIButtonType.RoundedRect);
            modalButton.Frame = new RectangleF(View.Frame.Width / 2 - 70, View.Bounds.Height / 2 + 20, 140, 40);
            modalButton.Font = UIFont.FromName("Helvetica", 22);
            modalButton.SetTitle("Modal", UIControlState.Normal);
            Add(modalButton);

            single = UIButton.FromType(UIButtonType.RoundedRect);
            single.Frame = new RectangleF(View.Frame.Width / 2 - 70, View.Bounds.Height / 2 + 60, 140, 40);
            single.Font = UIFont.FromName("Helvetica", 22);
            single.SetTitle("Single", UIControlState.Normal);
            Add(single);

            closeButton = UIButton.FromType(UIButtonType.RoundedRect);
            closeButton.Frame = new RectangleF(View.Bounds.Width / 2 - 70, View.Bounds.Height / 2 + 100, 140, 40);
            closeButton.Font = UIFont.FromName("Helvetica", 22);
            closeButton.SetTitle("Close", UIControlState.Normal);
            Add(closeButton);

            var set = this.CreateBindingSet<RedView, Core.ViewModels.RedViewModel>();
            set.Bind(splitViewButton).To(vm => vm.MenuCommand);
            set.Bind(modalButton).To(vm => vm.ModalCommand);
            set.Bind(single).To(vm => vm.SingleCommand);
            set.Bind(closeButton).To(vm => vm.CloseCommand);
            set.Apply();
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            splitViewButton.Frame = new RectangleF(View.Bounds.Width / 2 - 70, View.Bounds.Height / 2 - 20, 140, 40);
            modalButton.Frame = new RectangleF(View.Frame.Width / 2 - 70, View.Bounds.Height / 2 + 20, 140, 40);
            single.Frame = new RectangleF(View.Frame.Width / 2 - 70, View.Bounds.Height / 2 + 60, 140, 40);
            closeButton.Frame = new RectangleF(View.Bounds.Width / 2 - 70, View.Bounds.Height / 2 + 100, 140, 40);
        }
    }
}