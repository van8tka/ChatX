using ChatX.Interfaces.Service;
using ChatX.ViewModel;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatX.Service
{
    public class NavigationService : INavigationService
    {
        private readonly IChatXLog _log;
        private readonly IAuthService _authService;
        private Page _mainPage => Application.Current.MainPage;
        public NavigationService(IChatXLog log, IAuthService authService)
        {
            _log = log;
            _authService = authService;
        }

        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                _log.Info("PreviousPageViewModel", this);
                int countPagesInStack = _mainPage.Navigation.NavigationStack.Count;
                if (countPagesInStack < 2)
                    countPagesInStack = 2;
                return _mainPage.Navigation.NavigationStack[countPagesInStack - 2].BindingContext as ViewModelBase;
            }
        }

        public Task GoBackPage()
        {
            _log.Info("GoBackPage", this);
            return _mainPage.Navigation.PopAsync(true);
        }

        public async Task InitializeAsync()
        {
            try
            {
                _log.Info("InitializeAsync", this);
                await _authService.GetLocalDataAuth(this);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public Task NavigateToAsync<T>() where T : ViewModelBase
        {
            _log.Info("NavigateToAsync", this);
            return InternalNavigateToAsync(typeof(T), null);
        }

        public Task NavigateToAsync<T>(object param) where T : ViewModelBase
        {
            _log.Info("NavigateToAsync Param", this);
            return InternalNavigateToAsync(typeof(T), param);
        }

        public void RemoveBackStack()
        {
            try
            {
                _log.Info("RemoveBackStack", this);
                while (_mainPage.Navigation.NavigationStack.Count - 1 > 0)
                {
                    var page = _mainPage.Navigation.NavigationStack[0];
                    _mainPage.Navigation.RemovePage(page);
                }

            }
            catch (Exception e)
            {
                _log.Error(this, e);
            }
        }

        public void RemoveLastFromBackStack()
        {
            _log.Info("RemoveLastFromBackStack", this);
            int countPagesInStack = _mainPage.Navigation.NavigationStack.Count;
            if (countPagesInStack > 2)
                _mainPage.Navigation.RemovePage(_mainPage.Navigation.NavigationStack[countPagesInStack - 2]);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object param)
        {
            _log.Info("InternalNavigateToAsync", this);
            var page = CreatePage(viewModelType, param);
            if (_mainPage == null)
                Application.Current.MainPage = new NavigationPage(page);
            else
                await _mainPage.Navigation.PushAsync(page,true);
          await (page.BindingContext as ViewModelBase).InitializeAsync(param);
        }

        private Page CreatePage(Type viewModelType, object param)
        {
            _log.Info("CreatePage", this);
            var pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
                throw new Exception($"Can't locate  page type to {viewModelType}");
            return Activator.CreateInstance(pageType) as Page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            _log.Info("GetPageTypeForViewModel", this);
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            return Type.GetType(viewAssemblyName);
        }
    }
}
