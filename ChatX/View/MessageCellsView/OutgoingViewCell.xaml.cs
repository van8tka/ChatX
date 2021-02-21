using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChatX.View.Partials;
using Xamarin.Forms;
 
namespace ChatX.View.MessageCellsView
{
    public partial class OutgoingViewCell : ContentView
    {
        public OutgoingViewCell()
        {
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped(object s, EventArgs e)
        {
            var vm = (this.Parent?.Parent?.Parent as ChatView)?.ViewModel;
            if (vm != null)
                vm.TappedMessageCommand?.Execute(null);
        }
    }
}
