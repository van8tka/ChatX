using System;
using System.IO;
using Xamarin.Forms;

namespace ChatX.Model
{
    public class UserModel
    {
        public string Name { get; set; }
        private string _imageBase64Str;
        public string ImageBase64String { get=>_imageBase64Str;
            set {
                _imageBase64Str = value;
                Avatar = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_imageBase64Str)));
            }
        }
        public ImageSource Avatar
        {
            get;
            set;
        }
        private int _unread;
        public int UnreadCount { get=>_unread; 
            set {
                _unread = value;
                VisibleUnreadCount = _unread > 0;
            } 
        }    
        public bool VisibleUnreadCount { get; private set; }
        
    }
}
