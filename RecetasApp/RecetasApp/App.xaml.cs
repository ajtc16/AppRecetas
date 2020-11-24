using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecetasApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Modelo.User u = new Modelo.User();
            u.name = "Antonio";
            u.lastName = "Teran";
            u.id = 1;
            u.mail = "ajtc16@gmail.com";
            u.userName = "ajtc16";

            MainPage = new NavigationPage(new LoginFB());
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
