using System.Diagnostics;
using System.Threading.Tasks;
using ChatX.Interfaces.Service;
using ChatX.ViewModel;
using Xamarin.Forms;

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
        }

       async void Button_PropertyChanging(System.Object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            await Task.Yield();
            if (string.Equals(e.PropertyName, "ispressed", System.StringComparison.OrdinalIgnoreCase))
                PressedBtnSend = true;
        }
    }
    
}
