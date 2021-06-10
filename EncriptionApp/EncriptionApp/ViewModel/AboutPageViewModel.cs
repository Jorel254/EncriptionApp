using Acr.UserDialogs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EncriptionApp.ViewModel
{
    class AboutPageViewModel
    {
        public ICommand XamarinCommand { get; }
        public ICommand GitHubCommand { get; }
        public ICommand BuyMeACoffeCommand { get; }
        public AboutPageViewModel()
        {
            this.XamarinCommand = new Command(Xamarin);
            this.GitHubCommand = new Command(GitHub);
            this.BuyMeACoffeCommand = new Command(BuyMeACoffe);
        }
        private void GitHub() => OpenBrowser("https://github.com/Jorel254/EncriptionApp");
        private void Xamarin() => OpenBrowser("https://dotnet.microsoft.com/learn/xamarin/what-is-xamarin");
        private void BuyMeACoffe() => OpenBrowser("https://www.buymeacoffee.com/Jorel254");
        private async void OpenBrowser(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception e)
            {
                Log.Error(e.Message, nameof(OpenBrowser));
            }
        }
    }
}
