using System;

namespace ChatX.Interfaces.Service
{
    public interface IChatXLog
    {
        void Error(Exception e);
        void Error(string msg);
        void Error(Object item, string msg);
        void Error(Object item, Exception e);
        void Error(Object item, Exception e, string msg);
        void Info(string msg, Object item = null);
    }
}
