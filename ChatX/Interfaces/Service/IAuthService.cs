using ChatX.Model;
using System.Threading.Tasks;

namespace ChatX.Interfaces.Service
{
    public interface IAuthService
    {
        Task GetLocalDataAuth(INavigationService navigationService);
        Task WebAuth(LoginModel loginModel, INavigationService navigationService);
    }
}
