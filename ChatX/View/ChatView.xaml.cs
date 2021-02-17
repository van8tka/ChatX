using ChatX.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : ContentPage
    {
        public ChatView()
        {
            InitializeComponent();
            BindingContext = App.GetContainer.GetInstance<ChatViewModel>();
        }
    }
}