using System;
using System.Net;
using Thesis.Services;
using Thesis.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thesis
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
             MainPage = new AppShell();
         
        }

        protected override void OnStart()
        {
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                // Navigate to the main page
                Shell.Current.GoToAsync("//login");
                return false; // Stop the timer
            });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
