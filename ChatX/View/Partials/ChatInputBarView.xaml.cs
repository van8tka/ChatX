using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatX.ViewModel;
using Xamarin.Forms;

namespace ChatX.View.Partials
{
    public partial class ChatInputBarView : ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, source: chatTextInput));
        }

        private void chatTextInput_Completed(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                (this.Parent.Parent.BindingContext as ChatViewModel).OnSendCommand.Execute(null);
                chatTextInput.Focus();
            });
        }
        public void UnFocusEntry() => chatTextInput?.Unfocus();
    }
}
