using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Splitter.Touch.Views
{
    public class SingleView : BaseViewController
    {
        public SingleView()
        {
            TypeOfView = ViewType.SingleView;
        }
    }
}