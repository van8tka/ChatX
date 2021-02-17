using ChatX.Interfaces.Service;
using ChatX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChatX.Service
{
    public class SecureStorageService : ISecureStorageService
    {
        private readonly IChatXLog _log;
        public SecureStorageService(IChatXLog log) => _log = log;

        private const string SECURE_NAME = "name_chatx";
        private const string SECURE_PWD = "pwd_chatx";
        public async Task<LoginModel> GetLogin()
        {
            try
            {
                _log.Info("GetLogin", this);
                var login = await SecureStorage.GetAsync(SECURE_NAME);
                var password = await SecureStorage.GetAsync(SECURE_PWD);
                return new LoginModel { Login = login, Password = password };
            }
            catch(Exception er)
            {
                _log.Error(this, er);
                return new LoginModel();
            }
        }

        public async Task SetLogin(LoginModel loginModel)
        {
            try
            {
                _log.Info("SetLogin", this);
                await SecureStorage.SetAsync(SECURE_NAME, loginModel.Login);
               await SecureStorage.SetAsync(SECURE_PWD, loginModel.Password);
            }
            catch (Exception er)
            {
                _log.Error(this, er);
                throw;
            }
        }
    }
}
