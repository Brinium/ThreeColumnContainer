using System;
using Cirrious.MvvmCross.Touch.Views;
using Splitter.Panels;
using MonoTouch.UIKit;

namespace Splitter.Touch
{
    public class MasterPanelView : MvxViewController
    {
        public MasterPanelContainer MasterContainer { get; set; }

        public MasterPanelView()
        {
        }

        #region ViewLifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AddChildViewController(MasterContainer);
            View.AddSubview(MasterContainer.View);
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            if (MasterContainer != null)
                MasterContainer.ViewWillAppear(animated);
            base.ViewWillAppear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidAppear(bool animated)
        {
            if (MasterContainer != null)
                MasterContainer.ViewDidAppear(animated);
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Called whenever the Panel is about to be hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear(bool animated)
        {
            if (MasterContainer != null)
                MasterContainer.ViewWillDisappear(animated);
            base.ViewWillDisappear(animated);
        }

        /// <summary>
        /// Called every time after the Panel is hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidDisappear(bool animated)
        {
            if (MasterContainer != null)
                MasterContainer.ViewDidDisappear(animated);
            base.ViewDidDisappear(animated);
        }

        #endregion

        #region overrides to pass to container

        /// <summary>
        /// Called when the view will rotate.
        /// This override forwards the WillRotate callback on to each of the panel containers
        /// </summary>
        /// <param name="toInterfaceOrientation">To interface orientation.</param>
        /// <param name="duration">Duration.</param>
        public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillRotate(toInterfaceOrientation, duration);
            if (MasterContainer != null)
                MasterContainer.WillRotate(toInterfaceOrientation, duration);
        }

        /// <summary>
        /// Called after the view rotated
        /// This override forwards the DidRotate callback on to each of the panel containers
        /// </summary>
        /// <param name="fromInterfaceOrientation">From interface orientation.</param>
        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);
            if (MasterContainer != null)
                MasterContainer.DidRotate(fromInterfaceOrientation);
        }

        #endregion
    }
}

