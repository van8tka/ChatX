using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ChatX.iOS.Renderers;
using ChatX.View.Partials;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ChatInputBarView), typeof(ChatEntryRenderer))]
namespace ChatX.iOS.Renderers
{
    public class ChatEntryRenderer:ViewRenderer
    {
        private NSObject _keyboardShowObserver;
        private NSObject _keyboardHideObserver;

       
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
            } 
            if (e.OldElement != null)
               UnregisterForKeyboardNotifications();
        }

        private void UnregisterForKeyboardNotifications()
        {
            _keyboardShowObserver?.Dispose();
            _keyboardShowObserver = null;
            _keyboardHideObserver?.Dispose();
            _keyboardHideObserver = null;
        }

        private void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = UIKeyboard.Notifications.ObserveDidShow(OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = UIKeyboard.Notifications.ObserveDidHide(OnKeyboardHide);
        }

        private void OnKeyboardHide(object sender, UIKeyboardEventArgs e)
        {
            Debug.WriteLine("keyb_hide");
            if (Element != null) 
                Element.Margin = new Thickness(0);
        }

        private void OnKeyboardShow(object sender, UIKeyboardEventArgs e)
        {
             var keyInfo = (NSValue)e.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
             var frame = keyInfo.RectangleFValue.Size;
             Debug.WriteLine("keyb_show");
             if ( Element != null )
                 Element.Margin = new Thickness(0, 0, 0, frame.Height);
             if (ChatInputBarView.PressedBtnSend)//set flag - for next step keyboard will not show
                ChatInputBarView.PressedBtnSend = false;
        }
    }
}
