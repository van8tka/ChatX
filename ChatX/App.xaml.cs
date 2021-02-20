using ChatX.Service;
using System;
using SimpleInjector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChatX.Interfaces.Service;
using System.Threading.Tasks;
using ChatX.View;

using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace ChatX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Xamarin.Forms.Application
    {
        private static Container _container;
        public static Container GetContainer => _container;


        public App() {

            if (Device.RuntimePlatform == Device.Android)
            {
                 Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            }  
            InitializeComponent();
            _container = DiContainer.Register();
            InitStartPage();
        }

        private Task InitStartPage()
        {
            try
            {
                var navigation = _container.GetInstance<INavigationService>();
                return navigation.InitializeAsync();
            }
            catch(Exception e)
            {
                _container.GetInstance<IChatXLog>().Error(this, e);
                return Task.FromResult(false);
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
