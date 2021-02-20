using System;
using ChatX.Model;
using Xamarin.Forms;

namespace ChatX.View.MessageCellsView
{
    public class ChatTemplateSelector: DataTemplateSelector
    {
        private readonly DataTemplate _incomingTemplate;
        private readonly DataTemplate _outgoingTemplate;

        public ChatTemplateSelector()
        {
            _incomingTemplate = new DataTemplate(typeof(IncomingViewCell));
            _outgoingTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if(item is MessageModel message)
            {
                return message.Kind == Enums.MessagKindEnum.Incoming ? _incomingTemplate : _outgoingTemplate;
            }
            return null;
        }
    }
}
