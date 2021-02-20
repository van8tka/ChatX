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
        public void UnFocusEntry() => chatTextInput?.Unfocus();
        public async void SetFocus() {await Task.Delay(5); chatTextInput?.Focus(); }
    }
    
}
