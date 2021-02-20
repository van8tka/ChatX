using System;
using ChatX.Enums;

namespace ChatX.Model
{
    public class MessageModel
    {
         public string Text { get; set; }
         public string User { get; set; }
         public MessagKindEnum Kind { get; set; }
    }
}
