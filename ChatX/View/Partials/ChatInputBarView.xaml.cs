using System.Threading.Tasks;
using Xamarin.Forms;
using System;


namespace ChatX.View.Partials
{
    public partial class ChatInputBarView : ContentView
    {
        /// <summary>
        /// флаг для работы с клавиатурой - поднять и показывать постоянно для андроида и для ios
        /// false - hide keyboard
        /// true - show keyboard
        /// </summary>
        public static bool PressedBtnSend;
        private bool _isFirstSetFocus;

        public ChatInputBarView()
        {
            PressedBtnSend = false;
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, source: chatTextInput));
            _isFirstSetFocus = true;
        }

        private async void Button_PropertyChanging(Object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            if(Device.RuntimePlatform == Device.Android)
                await Task.Yield();
            if (string.Equals(e.PropertyName, "ispressed", System.StringComparison.OrdinalIgnoreCase))
            {
                PressedBtnSend = true;
            }
        }
        private void chatTextInput_Completed(Object sender, EventArgs e)
        {
            if(PressedBtnSend)
                chatTextInput.Focus();
        }

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
            if(Device.RuntimePlatform==Device.Android)
                if (PressedBtnSend)
                    PressedBtnSend = false;
            chatTextInput.Unfocus();
        }
    }
    
}
