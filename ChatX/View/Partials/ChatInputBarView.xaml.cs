using System.Diagnostics;
using System.Threading.Tasks;
using ChatX.Interfaces.Service;
using ChatX.ViewModel;
using Xamarin.Forms;
using System;
namespace ChatX.View.Partials
{
    public partial class ChatInputBarView : ContentView
    {

        public static bool PressedBtnSend;
        public ChatInputBarView()
        {
            PressedBtnSend = false;
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, source: chatTextInput));
            _isFirstSetFocus = true;
            isFocus = null;
        }

        private async void Button_PropertyChanging(Object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            await Task.Yield();
            if (string.Equals(e.PropertyName, "ispressed", System.StringComparison.OrdinalIgnoreCase))
            {
                PressedBtnSend = true;
            }
        }

        private bool? isFocus;
        private void chatTextInput_Completed(Object sender, EventArgs e)
        {
            if (isFocus == null || isFocus == true)
                chatTextInput.Focus();
            isFocus = true;
        }

        private void UnFocus(){ 
            isFocus = false;
            chatTextInput.Unfocus();
            isFocus = false;
        }

        private bool _isFirstSetFocus;
        private void chatTextInput_Focused(object sender, EventArgs e)
        {
            if (_isFirstSetFocus)
            {
                var vm = (this.Parent?.Parent as ChatView)?.ViewModel;
                if(vm!=null)
                    vm.TappedMessageCommand = new Command(HideKeyboard);
                _isFirstSetFocus = false;
            }  
        }
        private void HideKeyboard()
        {
            if (PressedBtnSend)
            {//установка флага для убирания клавиатуры - андроид
                PressedBtnSend = false;
            }
            UnFocus();
        }
    }
    
}
