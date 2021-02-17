using ChatX.ViewModel;
using Xamarin.Forms;

namespace ChatX.View
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = App.GetContainer.GetInstance<LoginViewModel>();
        }
    }
}
