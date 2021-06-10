using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EncriptionApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainPage = new MainPage();
            mainPage.BackgroundColor = Color.FromHex("#900C3F");
            mainPage.FlyoutBackgroundColor = Color.FromHex("#900C3F");
            MainPage = mainPage;
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
