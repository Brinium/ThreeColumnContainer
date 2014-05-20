using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Splitter.Core.ViewModels;

namespace Splitter.Touch.Views
{
    public class ModalView : MvxViewController, IMvxModalTouchView
    {
        public Size ViewSize
        {
            get
            {
                var size = new Size(300, 180);
                return size;
            }
        }
        public PointF ViewPosition
        {
            get
            {
                var point = new PointF(UIScreen.MainScreen.ApplicationFrame.Width / 2, UIScreen.MainScreen.ApplicationFrame.Height / 2);
                return point;
            }
        }

        public ModalView()
        {
            this.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
            this.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
            this.ModalInPopover = true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;

            var closeButton = UIButton.FromType(UIButtonType.RoundedRect);
            closeButton.Frame = new RectangleF(ViewSize.Width / 2 - 50, ViewSize.Height / 2 - 20, 100, 40);
            closeButton.Font = UIFont.FromName("Helvetica", 22);
            closeButton.SetTitle("Close", UIControlState.Normal);
            Add(closeButton);

            var set = this.CreateBindingSet<ModalView, ModalViewModel>();
            set.Bind(closeButton).To(vm => vm.CloseCommand);
            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var viewFrame = View.Frame;

            this.View.Superview.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
            this.View.Superview.Bounds = new RectangleF(0, 0, ViewSize.Width, ViewSize.Height);
            this.View.Superview.Center = ViewPosition;
        }
    }
}