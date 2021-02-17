using ChatX.Model;
using System.Threading.Tasks;

namespace ChatX.Interfaces.Service
{
    public interface ISessionService
    {
        Task<SessionModel> GetSession();
        Task SetSession(SessionModel session);
    }
}
