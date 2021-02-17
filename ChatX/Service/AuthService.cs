using ChatX.Interfaces.Service;
using ChatX.Model;
using ChatX.ViewModel;
using System;
using System.Threading.Tasks;

namespace ChatX.Service
{
    public class AuthService : IAuthService
    {
      
        private ISecureStorageService _secureStorage;
        private ISessionService _sessionService;
        private IWebService _webServcie;
        private IChatXLog _log;
        public AuthService(ISecureStorageService secureStorageService, ISessionService sessionService,IWebService webService ,IChatXLog log)
        {
            _secureStorage = secureStorageService;
            _sessionService = sessionService;
            _webServcie = webService;
            _log = log;
        }

        public async Task GetLocalDataAuth(INavigationService navigationService)
        {
            try
            {
                _log.Info("GetLocalDataAuth", this);
                var loginModel = await _secureStorage.GetLogin();
                if (string.IsNullOrEmpty(loginModel.Login))
                    await navigationService.NavigateToAsync<LoginViewModel>();
                else
                    await WebAuth(loginModel, navigationService);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public async Task WebAuth(LoginModel loginModel, INavigationService navigationService)
        {
            try
            {
                _log.Info("WebAuth", this);
                var session = await _webServcie.SignIn(loginModel);
#if __IOS__ && DEBUG
                await _sessionService.SetSession(session);
#endif
                await navigationService.NavigateToAsync<ContactsViewModel>();
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
