using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Thesis.Core;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using Thesis.Dto;
using System.Threading.Tasks;

namespace Thesis.Services
{
    public class PointService : IPointService
    {
        private HttpClient _httpClient;
        public PointService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;

            _httpClient = new HttpClient(httpClientHandler);
        }


            public async Task<IEnumerable<PointDto>> GetAllPins()
        {

            HttpResponseMessage response = await _httpClient.GetAsync(GlobalValues.UrlAddress + "api/Points");


            var resultHttp = await response.Content.ReadAsStringAsync();
            var getObjectPin = JsonConvert.DeserializeObject<List<PointDto>>(resultHttp);

            return getObjectPin;
        }
    }
}
