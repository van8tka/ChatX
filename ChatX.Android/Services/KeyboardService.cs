using System;
using ChatX.Droid.Services;
using ChatX.Interfaces.Service;
using Xamarin.Forms;

[assembly:Dependency(typeof(KeyboardService))]
namespace ChatX.Droid.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideKeyboardForce(){  }
    }
}