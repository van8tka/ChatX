using System;
using System.Linq;
using System.Windows.Input;
using ChatX.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : ContentPage
    {
        
        private readonly ChatViewModel _chatVM;
        public ChatView()
        {
            InitializeComponent();
            _chatVM = App.GetContainer.GetInstance<ChatViewModel>();
            BindingContext = _chatVM;
            _chatVM.OnScrollToLastCommand = new Command(ScrollToLast);
        }

        private void ScrollToLast(object obj)
        {
            var msgLast = _chatVM.Messages.LastOrDefault();
            if (msgLast != null)
                ChatList.ScrollTo(msgLast, ScrollToPosition.End, true);
        }

        private void ChatList_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }
    }
}