using Acr.UserDialogs;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Thesis.Core;
using Thesis.Dto;
using Thesis.Services;
using Thesis.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Thesis.Views
{
    public partial class AboutPage : ContentPage
    {
        private HttpClient _httpClient;
        private IPointService _pointService;
        public AboutPage()
        {
            InitializeComponent();
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;

            _httpClient = new HttpClient(httpClientHandler);

            var position = new Position(37.79752, -122.40183);//razglejdah taka sega nameri kak 
            var mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1));

            navMap.MoveToRegion(mapSpan);
            _pointService = new PointService();
        
        }

        
        private async void OnMapTapped(object sender, MapClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(GlobalValues.Token))
            {
                string result = await DisplayPromptAsync("Точка", "Найменование на точката:");

                Pin pin = new Pin
                {
                    Label = result,
                    Position = new Position(e.Position.Latitude, e.Position.Longitude)
                };
                navMap.Pins.Add(pin);
                var requestData = new { name = result, latitude = e.Position.Latitude.ToString(), longitude = e.Position.Longitude.ToString() };
                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalValues.Token);
                HttpResponseMessage response = await _httpClient.PostAsync(GlobalValues.UrlAddress + "api/Points", content);
                var resultHttp = await response.Content.ReadAsStringAsync();
            }
            else
            {
                await DisplayAlert("Грешка", "Първо трябва да бъдете аутентикиран", "OK");

            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var points = await _pointService.GetAllPins();
            foreach (var pin in points)
            {
                navMap.Pins.Add(new Pin()
                {
                    Label = pin.Name,
                    Position = new Position(Convert.ToDouble(pin.Latitude), Convert.ToDouble(pin.Longitude)),
                });

            }

        }
    }


}