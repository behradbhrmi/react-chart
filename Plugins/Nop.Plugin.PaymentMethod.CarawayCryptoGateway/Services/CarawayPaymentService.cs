using Newtonsoft.Json;
using Nop.Core;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services.RequestResponse;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services
{
    public interface ICarawayPaymentService
    {
        Task<CreatePaymentResponseModel> CreatePayment(CreatePaymentRequestModel request);
        Task<VerifyResponseModel> Verify(string sessionId);
    }


    public class CarawayPaymentService : ICarawayPaymentService
    {
        private readonly CarawayPaymentSettings _settings;
        private readonly HttpClient _httpClient;

        public CarawayPaymentService(HttpClient httpClient, CarawayPaymentSettings settings)
        {
            _settings = settings;
            _httpClient = httpClient;
        }

        private void PrepareHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-API-KEY", _settings.ApiKey);
        }
        public async Task<CreatePaymentResponseModel> CreatePayment(CreatePaymentRequestModel request)
        {
            PrepareHttpClient(_httpClient);
            var content = JsonConvert.SerializeObject(request);

            var response = await _httpClient.PostAsync(_settings.PaymentOpenUrl, new StringContent(content, Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var responseModel = JsonConvert.DeserializeObject<CreatePaymentResponseModel>(resultString);
                return responseModel;
            }

            throw new NopException(resultString);
        }

        public async Task<VerifyResponseModel> Verify(string sessionId)
        {
            PrepareHttpClient(_httpClient);
            var url = string.Format(_settings.PaymentVerifyUrl, sessionId);

            var response = await _httpClient.PostAsync(url, null);
            var resultString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var responseModel = JsonConvert.DeserializeObject<VerifyResponseModel>(resultString);
                return responseModel;
            }

            throw new NopException(resultString);
        }
    }
}
