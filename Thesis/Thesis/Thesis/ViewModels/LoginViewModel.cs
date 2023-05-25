using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Thesis.Core;
using Thesis.Dto;
using Thesis.Views;
using Xamarin.Forms;

namespace Thesis.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private HttpClient _httpClient;
        public string UserName { get; set; }
        public string Password { get; set; }
        public Command LoginCommand { get; }
        public Command ContiniueAsGestCommand { get; }
        public INavigation Navigation { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;

            _httpClient = new HttpClient(httpClientHandler);

                this.Navigation = navigation;
            LoginCommand = new Command(OnLoginClicked);
            ContiniueAsGestCommand = new Command(ConitiniueAsGest);
        }
        private async void ConitiniueAsGest(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

        }
        private async void OnLoginClicked(object obj)
        {
            var currentPage = Application.Current.MainPage;

            try
            {
                
                string jsonData = @"{""userName"" : """ + UserName + @""", ""password"" : """ + Password + @"""}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(GlobalValues.UrlAddress + "api/Login", content);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var tokenJson = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<LoginDto>(tokenJson);
                    GlobalValues.IsLogin = true;
                    GlobalValues.Token = token.Token;
                    
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                if(response.StatusCode ==System.Net.HttpStatusCode.Unauthorized) {
                    
                    // Display the alert
                    await currentPage.DisplayAlert("Грешка", "Моля проверете потребителското име и паролата", "OK");
                }

            }
            catch (Exception ex)
            {
                await currentPage.DisplayAlert("Грешка", $"Възникна неочаквана грешка {ex.ToString()}", "OK");


            }
            
        }
    }
}
