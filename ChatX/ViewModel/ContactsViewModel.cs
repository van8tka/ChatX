using ChatX.Interfaces.Service;
using ChatX.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChatX.ViewModel
{
    public class ContactsViewModel: ViewModelBase
    {
        private readonly IWebService _webService;
        private readonly ISessionService _session;
        public ContactsViewModel(INavigationService navigation, IChatXLog log, IWebService webService, ISessionService session) : base(navigation, log) {
            _webService = webService;
            _session = session;
        }

        private ObservableCollection<UserModel> _contacts;
        public ObservableCollection<UserModel> Contacts
        { 
            get => _contacts;
            set {
                _contacts = value;
            OnPropertyChanged(nameof(Contacts)); }
        }

       

        public override async Task InitializeAsync(object parameter)
        {
            try
            {
                IsBusy = true;
                var session = await _session.GetSession();
                Contacts = new ObservableCollection<UserModel>(await _webService.GetUsers(session.Token));
                Contacts.ElementAt(3).UnreadCount = 4;
                await base.InitializeAsync(parameter);
            }
            catch(Exception e)
            {
                Log.Error(this, e);
            }
        }
    }
}
