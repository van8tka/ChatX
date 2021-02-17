using ChatX.Interfaces.Service;
using ChatX.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChatX.Service
{
    public class SessionService : ISessionService
    {
        private const string KEY_SESSION_USERNAME = "session_name";
        private const string KEY_SESSION_AVATAR = "session_avatar";
        private const string KEY_SESSION_TOKEN = "session_token";
        private readonly IChatXLog _log;
        public SessionService(IChatXLog log)
        {
            _log = log;
            _cashedSessionModel = null;
        }

        private SessionModel _cashedSessionModel { get; set; }

        public async Task<SessionModel> GetSession()
        {
            try
            {
                if (_cashedSessionModel != null)
                {
                    _log.Info("GetSession CASHED", this);
                    return _cashedSessionModel;
                }
                _log.Info("GetSession UNCASHED", this);
                var name = await SecureStorage.GetAsync(KEY_SESSION_USERNAME);
                var avatar = await SecureStorage.GetAsync(KEY_SESSION_AVATAR);
                var token = await SecureStorage.GetAsync(KEY_SESSION_TOKEN);
                _cashedSessionModel = new SessionModel { CurrentUser = new UserModel { Name = name, ImageBase64String = avatar }, Token = token };
                return _cashedSessionModel;
            }
            catch (Exception e)
            {
                throw;
            }
           
        }

        private bool RemoveSession()
        {
            try
            {
                _log.Info("RemoveSession", this);
                return SecureStorage.Remove(KEY_SESSION_TOKEN);
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public async Task SetSession(SessionModel session)
        {
            try
            {
                _log.Info("SetSession", this);
                var name = await SecureStorage.GetAsync(KEY_SESSION_USERNAME);
                if (string.IsNullOrEmpty(name))
                {
                    await SecureStorage.SetAsync(KEY_SESSION_USERNAME, session.CurrentUser.Name);
                    await SecureStorage.SetAsync(KEY_SESSION_AVATAR, session.CurrentUser.ImageBase64String);
                    await SecureStorage.SetAsync(KEY_SESSION_TOKEN, session.Token);
                }
                else
                {
                   await UpdateSession(session.Token);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task UpdateSession(string token)
        {
            try
            {
                _log.Info("UpdateSession", this);
                if (SecureStorage.Remove(KEY_SESSION_TOKEN))
                    await SecureStorage.SetAsync(KEY_SESSION_TOKEN, token);
            }
            catch(Exception e)
            {
                throw;
            }
           
        }
    }
}
