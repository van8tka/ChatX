using System.Diagnostics;
using ChatX.Interfaces.Service;
using ChatX.ViewModel;
using Xamarin.Forms;

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