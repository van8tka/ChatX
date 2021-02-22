using System;
using System.Diagnostics;
using ChatX.Interfaces.Service;
using ChatX.iOS.Services;
using UIKit;
using Xamarin.Forms;

[assembly:Dependency(typeof(KeyboardService))]
namespace ChatX.iOS.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideKeyboardForce()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}
