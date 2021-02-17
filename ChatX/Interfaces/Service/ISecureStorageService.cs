using ChatX.Model;
using System.Threading.Tasks;

namespace ChatX.Interfaces.Service
{
    public interface ISecureStorageService
    {
        Task<LoginModel> GetLogin();
        Task SetLogin(LoginModel loginModel);
    }
}
