using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using PetManager.Diagnostics;

namespace PetManager.Services
{
    public class HttpClientService : IHttpClientService
    {
        public string Get()
        {
            string result = string.Empty;

            result = GetExternalResponse().Result;
            return result;
        }

        private async Task<string> GetExternalResponse()
        {
            var client = new HttpClient();
            string result = null;
            try
            {
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get,"http://agl-developer-test.azurewebsites.net/people.json"));
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException();

                result = await response.Content.ReadAsStringAsync();
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine(ex);
                Logger.LogError(ex);
                return null;
            }
            return result;
        }
    }
}
