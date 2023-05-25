using System;
using System.Collections.Generic;
using Thesis.ViewModels;
using Thesis.Views;
using Xamarin.Forms;

namespace Thesis
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public bool isLogin = false;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
        
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
