using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Splitter.Touch.Views
{
    public class MenuView : BaseViewController
    {
        public MenuView()
        {
            TypeOfView = ViewType.MenuView;
        }
    }
}