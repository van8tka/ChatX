using ChatX.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatX.View
{
    public partial class ContactsView : ContentPage
    {
        public ContactsView()
        {
            InitializeComponent();
            BindingContext = App.GetContainer.GetInstance<ContactsViewModel>();
        }

        
    }
}