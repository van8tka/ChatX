using ChatX.ViewModel;
using System.Threading.Tasks;

namespace ChatX.Interfaces.Service
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<T>() where T : ViewModelBase;
        Task NavigateToAsync<T>(object param) where T : ViewModelBase;
        void RemoveLastFromBackStack();
        void RemoveBackStack();
        Task GoBackPage();
    }
}
