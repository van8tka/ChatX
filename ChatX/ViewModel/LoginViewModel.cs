using ChatX.Interfaces.Service;
using ChatX.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatX.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        private readonly IAuthService _authService;
        public LoginViewModel(INavigationService navigation,IChatXLog log, IAuthService authService) : base(navigation, log) {
            _authService = authService;
            EnterCommand = new Command(async()=>await Enter());
            LoginModelField = new LoginModel();
        }

        public ICommand EnterCommand { get; private set; }

        public override Task InitializeAsync(object parameter)
        {
            IsBusy = true;
            return base.InitializeAsync(parameter);
        }


        private LoginModel _loginModel;
        
        public LoginModel LoginModelField {
            get => _loginModel;
            set
            {
                _loginModel = value;
                OnPropertyChanged(nameof(LoginModelField));
            }
        }

        private async Task Enter()
        {
            try
            {
                await _authService.WebAuth(LoginModelField, NavigationService);
            }
            catch(Exception e)
            {
                Log.Error(this, e);
            }
        }
    }
}
