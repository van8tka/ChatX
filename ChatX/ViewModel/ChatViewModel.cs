using ChatX.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatX.ViewModel
{
    public class ChatViewModel: ViewModelBase
    {
        public ChatViewModel(INavigationService navigation, IChatXLog log) : base(navigation, log) { }

        public override Task InitializeAsync(object parameter)
        {
            IsBusy = true;
            return base.InitializeAsync(parameter);
        }
    }
}
