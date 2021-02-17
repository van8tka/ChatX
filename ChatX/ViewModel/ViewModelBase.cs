using ChatX.Interfaces.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ChatX.ViewModel
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; private set; }
        protected IChatXLog Log { get; private set; }
        public ViewModelBase(INavigationService navigationService, IChatXLog log)
        {
            NavigationService = navigationService;
            Log = log;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task InitializeAsync(object parameter)
        {
            IsBusy = false;
            return Task.FromResult(false);
        }
    }
}
