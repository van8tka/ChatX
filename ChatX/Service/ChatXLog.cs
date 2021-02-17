using ChatX.Interfaces.Service;
using System;
using System.Diagnostics;

namespace ChatX.Service
{
    public class ChatXLog : IChatXLog
    {
        private const string TYPE_MSG_ERROR = "[ERROR]";
        private const string TYPE_MSG_INFO = "[INFO]";

        public void Error(Exception e)
        {
            HandleError(TYPE_MSG_ERROR+" "+e.Message);
        }

        public void Error(string msg)
        {
            HandleError(TYPE_MSG_ERROR+" "+msg);
        }

        public void Error(object item, string msg)
        {
            HandleError(TYPE_MSG_ERROR +" ["+ item.GetType().ToString()+ "] " + msg);
        }

        public void Error(object item, Exception e)
        {
            Error(item, e.Message);
        }

        public void Error(object item, Exception e, string msg)
        {
            Error(item, e.Message+" "+msg);
        }

        public void Info(string msg, object item = null)
        {
            string t = item == null ? string.Empty : item.GetType().ToString();
            HandleError(TYPE_MSG_INFO + " " + t +" "+ msg, false);
        }


        private void HandleError(string msg, bool isError = true)
        {
            string dateNow = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            Debug.WriteLine("["+dateNow+"]"+msg);
            if(isError)
                Debugger.Break();
        }
    }
}
