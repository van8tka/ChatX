
using System.Linq;
using ChatX.ViewModel;
using Xamarin.Forms;


namespace ChatX.View
{

    public partial class ChatView : ContentPage
    {
        private readonly ChatViewModel _chatVM;
        public ChatView()
        {
            InitializeComponent();
            _chatVM = App.GetContainer.GetInstance<ChatViewModel>();
            BindingContext = _chatVM;
           
            _chatVM.OnSetFocusCommand = new Command(() => chatInput.SetFocus());
        }

       
        private void ChatList_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }
    }
}