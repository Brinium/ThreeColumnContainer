using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Splitter.Touch.Views.PanelContainers;

namespace Splitter.Touch.Views
{
    public class BaseViewController : MvxViewController
    {
        public ViewType TypeOfView { get; set; }
    }

    public enum ViewType
    {
        SingleView,
        MenuView,
        SubMenuView,
        DetailView
    }
}