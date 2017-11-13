using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EmailProviders.Models;
using EmailProviders.Service.Config;
using EmailProviders.Services;
using Newtonsoft.Json;

namespace EmailProviders.Helper
{
    public class MailGunProvider : IEmailProvider
    {
        MailGunEmailSetting _mailGun;
        public MailGunProvider()
        {
            _mailGun = ConfigurationService.Instance.MailGunEmailSetting;
        }

        public async Task<HttpResponseMessage> SendEmailAsync(Email model)
        {
            string responseData = string.Empty;
            using (var client = new HttpClient { BaseAddress = new Uri(_mailGun.BaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(_mailGun.ApiKey)));
                   //_mailGun.ApiKey);

                var content = new FormUrlEncodedContent(new[]
                    {
                    
                   new KeyValuePair<string, string>("from", _mailGun.From),

                    !string.IsNullOrEmpty(model.to)? new KeyValuePair<string, string>("to", model.to):new KeyValuePair<string, string>(),
                    !string.IsNullOrEmpty(model.bcc)?  new KeyValuePair<string, string>("bcc", model.bcc):new KeyValuePair<string, string>(),
                    !string.IsNullOrEmpty(model.cc)? new KeyValuePair<string, string>("cc", model.cc):new KeyValuePair<string, string>(),
                    !string.IsNullOrEmpty(model.subject)? new KeyValuePair<string, string>("subject", model.subject):new KeyValuePair<string, string>(),
                    !string.IsNullOrEmpty(model.text) ?  new KeyValuePair<string,string>("text", model.text):new KeyValuePair<string, string>(),
                   // !string.IsNullOrEmpty(model.bcc) || !string.IsNullOrEmpty(model.cc) ?  new KeyValuePair<string, string>("message", model.text):new KeyValuePair<string, string>(),

                });
                HttpResponseMessage response = client.PostAsync(_mailGun.RequestUri, content).Result;
                using (HttpContent rescontent = response.Content)
                {
                    responseData = await rescontent.ReadAsStringAsync();
                }
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(responseData);
                return response;

            }
        }

      
    }
}
