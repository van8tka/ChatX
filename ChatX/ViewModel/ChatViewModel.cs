using ChatX.Interfaces.Service;
using ChatX.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatX.ViewModel
{
    public class ChatViewModel: ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private SessionModel _session;

        public ChatViewModel(INavigationService navigation, IChatXLog log, ISessionService session) : base(navigation, log)
        {
            _sessionService = session;
            OnSendCommand = new Command(SendMsg);
            Messages = new ObservableCollection<MessageModel>();
        }


        private void SendMsg()
        {
            if (string.IsNullOrWhiteSpace(TextToSend))
                return;
            var newMsg = new MessageModel { Text = TextToSend, User = _session.CurrentUser.Name , Kind = Enums.MessagKindEnum.Outgoing };
            MessageAddToChat(newMsg);
            TextToSend = string.Empty;
        }

        private void MessageAddToChat(MessageModel msg)
        {
            Messages.Add(msg);
            if (OnScrollToLastCommand != null)
                OnScrollToLastCommand.Execute(null);
        }

        public ICommand OnScrollToLastCommand { get; internal set; }
        public ICommand OnSendCommand { get; private set; }
        private ObservableCollection<MessageModel> _messages;
        public ObservableCollection<MessageModel> Messages
        {
            get=>_messages;
            set { _messages = value;OnPropertyChanged(nameof(Messages)); }
        }
        private string _textSend;
        public string TextToSend
        {
            get=>_textSend;
            set { _textSend = value; OnPropertyChanged(nameof(TextToSend)); }
        }
        private string _chatName;
        public string ChatName
        {
            get => _chatName;
            set { _chatName = value; OnPropertyChanged(nameof(ChatName)); }
        }

       

        public override async Task InitializeAsync(object parameter)
        {
            IsBusy = true;
            ChatName = (parameter as UserModel)?.Name;
            Messages = GetMessages();
            _session = await _sessionService.GetSession();
            await FakeSender();
            await base.InitializeAsync(parameter);
        }


        private async Task FakeSender()
        {
            for (int i=0; i < 20;i++)
            {
                await Task.Delay(3000);
                var msg = new MessageModel { Text = $"Hi niger! This message is number {i}", User = ChatName, Kind = Enums.MessagKindEnum.Incoming };
                MessageAddToChat(msg);
            }
        }

        private ObservableCollection<MessageModel> GetMessages()
        {
            return new ObservableCollection<MessageModel>()
            {
                new MessageModel { Text = "Hi!", User = ChatName , Kind=Enums.MessagKindEnum.Incoming},
                new MessageModel { Text = "How are you?", User = ChatName ,Kind=Enums.MessagKindEnum.Incoming},
                new MessageModel { Text = "Are you free?", User = ChatName ,Kind=Enums.MessagKindEnum.Incoming} 
            };
        }
    }
}
