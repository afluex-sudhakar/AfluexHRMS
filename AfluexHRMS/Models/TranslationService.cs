using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class TranslateService : ITranslateText
    {  
        public async Task<string> Translate(string uri, string text, string key)
        {
            System.Object[] body = new System.Object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);

                return result;
            }
        }
    }

  
        interface ITranslateText
        {
            Task<string> Translate(string uri, string text, string key);
        }
    
}