using System.Threading.Tasks;
using ChatX.Interfaces.Service;
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
      
    }
    
}
