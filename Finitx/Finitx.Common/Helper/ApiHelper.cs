using Finitx.Common.Dto;
using Finitx.Common.Models;
using Newtonsoft.Json;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Helper
{
    public class ApiHelper : IApiHelper
    {
        private readonly FinitxLogDataSetting _finitxLogDataSetting;
        private readonly ISettingService _settingService;
        private readonly ILogger _logger;
        public ApiHelper(
            FinitxLogDataSetting finitxLogDataSetting,
            ISettingService settingService,
            ILogger logger)
        {
            _finitxLogDataSetting = finitxLogDataSetting;
            _settingService = settingService;
            _logger = logger;
        }
        private Uri GetUrl(string url)
        {
            Uri baseUri = new Uri(_finitxLogDataSetting.BaseUrl);
            Uri myUri = new Uri(baseUri, url);
            return myUri;
        }
        public async Task<TResponse> PostData<TResponse, TRequest>(string url, TRequest requestData)
        {
            var success = false;
            var retry = 0;
            TResponse response = default;
            while (!success && retry < 5)
            {
                retry++;

                try
                {
                    var strResult = "{}";
                    var client = new HttpClient();

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_finitxLogDataSetting.LoginToken}");
                    var result = await client.PostAsync(GetUrl(url), new StringContent(JsonConvert.SerializeObject(requestData), Encoding.Unicode, "application/json"));
                    if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        var token = await RestToken();
                        client.DefaultRequestHeaders.Remove("Authorization");
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                        result = await client.PostAsync(GetUrl(url), new StringContent(JsonConvert.SerializeObject(requestData), Encoding.Unicode, "application/json"));
                    }
                    success = result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created;
                    strResult = await result.Content.ReadAsStringAsync();
                    if (typeof(TResponse).Name == "String" || typeof(TResponse).IsValueType)
                    {
                        response = (TResponse)Convert.ChangeType(strResult, typeof(TResponse));
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<TResponse>(strResult ?? "{}");
                    }
                    await _logger.InformationAsync($"Post Finitx Data url:\"{GetUrl(url)}\" data:\"{JsonConvert.SerializeObject(requestData) }\" Result: {strResult} Try:{retry}");
                }
                catch (Exception ex)
                {
                    await _logger.ErrorAsync("Post Finitx Data Failed", ex);
                }
            }


            return response;
        }

        public async Task<TResponse> PutData<TResponse, TRequest>(string url, TRequest requestData)
        {
            TResponse response = default;
            var success = false;
            var retry = 0;
            while (!success && retry < 5)
            {
                retry++;
                try
                {
                    var strResult = "{}";
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_finitxLogDataSetting.LoginToken}");
                    var result = await client.PutAsync(GetUrl(url), new StringContent(JsonConvert.SerializeObject(requestData), Encoding.Unicode, "application/json"));
                    if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        var token = await RestToken();
                        client.DefaultRequestHeaders.Remove("Authorization");
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                        result = await client.PutAsync(GetUrl(url), new StringContent(JsonConvert.SerializeObject(requestData), Encoding.Unicode, "application/json"));
                    }
                    success = result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created;
                    strResult = await result.Content.ReadAsStringAsync();
                    if (typeof(TResponse).Name == "String" || typeof(TResponse).IsValueType)
                    {
                        response = (TResponse)Convert.ChangeType(strResult, typeof(TResponse));
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<TResponse>(strResult ?? "{}");
                    }
                    await _logger.InformationAsync($"Put Finitx Data url:\"{url}\" data:\"{JsonConvert.SerializeObject(requestData) }\" Result: {strResult} Try:{retry}");

                }
                catch (Exception ex)
                {
                    await _logger.ErrorAsync("Put Finitx Data Failed", ex);
                }

            }

            return response;
        }

        private async Task<string> RestToken()
        {
            var resultToken = "";
            try
            {
                var client = new HttpClient();
                var result = await client.PostAsync(_finitxLogDataSetting.LoginApiUrl, new StringContent(JsonConvert.SerializeObject(
                    new FinitxLoginDto
                    {
                        Username = _finitxLogDataSetting.Username,
                        Password = _finitxLogDataSetting.Password
                    }), Encoding.Unicode, "application/json"));
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var strResult = await result.Content.ReadAsStringAsync();
                    resultToken = strResult;// _finitxLogDataSetting.LoginToken = JsonConvert.DeserializeObject<FinitxLoginResponseDto>( strResult)?.Data.Token;
                    _finitxLogDataSetting.LoginToken = resultToken;
                    await _settingService.SaveSettingOverridablePerStoreAsync(_finitxLogDataSetting, x => x.LoginToken, true, 1, true);
                    await _settingService.ClearCacheAsync();
                }
                else
                {
                    await _logger.ErrorAsync($"login to finitx failed :({await result.Content.ReadAsStringAsync()})");
                }

            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync("login to finitx failed", ex);
            }
            return resultToken;
        }
    }
}
