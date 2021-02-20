using ChatX.ViewModel;
using Xamarin.Forms;


namespace ChatX.View
{

    public partial class ChatView : ContentPage
    {
        private ChatViewModel _vm;
        public ChatViewModel ViewModel { get; } = App.GetContainer.GetInstance<ChatViewModel>();
        public ChatView()
        { 
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}