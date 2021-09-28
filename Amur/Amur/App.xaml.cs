using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amur
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           
            var login = new LogIn();
            MainPage = new NavigationPage(login);
            NavigationPage.SetHasNavigationBar(login, false);
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
