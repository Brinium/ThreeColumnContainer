using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Splitter.Touch.Views
{
    [Register("MenuView")]
    public class MenuView : BaseViewController
    {
        public MenuView() : base()
        {
            TypeOfView = ViewType.MenuView;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.LightGray;
            
            var blue = UIButton.FromType(UIButtonType.RoundedRect);
            blue.Frame = new RectangleF(0, 10, 98, 40);
            blue.Font = UIFont.FromName("Helvetica", 22);
            blue.SetTitle("Blue", UIControlState.Normal);
            Add(blue);

            var red = UIButton.FromType(UIButtonType.RoundedRect);
            red.Frame = new RectangleF(0, 50, 98, 40);
            red.Font = UIFont.FromName("Helvetica", 22);
            red.SetTitle("Red", UIControlState.Normal);
            Add(red);

            var model = UIButton.FromType(UIButtonType.RoundedRect);
            model.Frame = new RectangleF(0, 90, 98, 40);
            model.Font = UIFont.FromName("Helvetica", 22);
            model.SetTitle("Modal", UIControlState.Normal);
            Add(model);

            var single = UIButton.FromType(UIButtonType.RoundedRect);
            single.Frame = new RectangleF(0, 130, 98, 40);
            single.Font = UIFont.FromName("Helvetica", 22);
            single.SetTitle("Single", UIControlState.Normal);
            Add(single);

            var sub = UIButton.FromType(UIButtonType.RoundedRect);
            sub.Frame = new RectangleF(0, 170, 98, 40);
            sub.Font = UIFont.FromName("Helvetica", 22);
            sub.SetTitle("Sub", UIControlState.Normal);
            Add(sub);

            var set = this.CreateBindingSet<MenuView, Core.ViewModels.MenuViewModel>();
            set.Bind(blue).To(vm => vm.BlueCommand);
            set.Bind(red).To(vm => vm.RedCommand);
            set.Bind(model).To(vm => vm.ModalCommand);
            set.Bind(single).To(vm => vm.SingleCommand);
            set.Bind(sub).To(vm => vm.SubCommand);
            set.Apply();
        }
    }
}