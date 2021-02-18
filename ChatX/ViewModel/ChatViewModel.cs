using ChatX.Interfaces.Service;
using ChatX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatX.ViewModel
{
    public class ChatViewModel: ViewModelBase
    {
        public ChatViewModel(INavigationService navigation, IChatXLog log) : base(navigation, log) { }

        private string _chatName;
        public string ChatName
        {
            get => _chatName;
            set { _chatName = value; OnPropertyChanged(nameof(ChatName)); }
        }

        public override Task InitializeAsync(object parameter)
        {
            IsBusy = true;
            ChatName = (parameter as UserModel)?.Name;
            return base.InitializeAsync(parameter);
        }
    }
}
