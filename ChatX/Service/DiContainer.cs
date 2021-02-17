using ChatX.Interfaces.Service;
using ChatX.ViewModel;
using SimpleInjector;

namespace ChatX.Service
{
    public static class DiContainer
    {
        public static Container Register()
        {
            var c = new Container();
            c.RegisterSingleton<IChatXLog, ChatXLog>();
            c.Register<ISecureStorageService, SecureStorageService>(Lifestyle.Transient);
            c.Register<ISessionService, SessionService>(Lifestyle.Singleton);
            c.Register<IWebService, WebService>(Lifestyle.Transient);
            c.Register<IAuthService, AuthService>(Lifestyle.Transient);
            c.Register<INavigationService, NavigationService>(Lifestyle.Transient);           
            c.Register<LoginViewModel>();
            c.Register<ContactsViewModel>();
            c.Register<ChatViewModel>();
            return c;
        }
    }
}
