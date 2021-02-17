using ChatX.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatX.Interfaces.Service
{
    public interface IWebService
    {
        Task<SessionModel> SignIn(LoginModel loginModel);
        Task<IEnumerable<UserModel>> GetUsers(string token);
    }
}
