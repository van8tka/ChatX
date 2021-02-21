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

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if(ChatInputBarView.PressedBtnSend)
            {// установка флага для убирания клавиатуры -андроид
                ChatInputBarView.PressedBtnSend = false;
            } 
        }
    }
}
