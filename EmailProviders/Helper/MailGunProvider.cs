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
            using (var client = new HttpClient { BaseAddress = new Uri(_mailGun.BaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(_mailGun.ApiKey)));

                var content = new FormUrlEncodedContent(new[]
                    {
                    //new KeyValuePair<string, string>("api_key", _mailGun.APIKey),
                    //new KeyValuePair<string, string>("api_user", _mailGun.APIUser),
                    new KeyValuePair<string, string>("from", _mailGun.From),
                    new KeyValuePair<string, string>("to", model.to),
                    new KeyValuePair<string, string>("bcc", model.bcc),
                    new KeyValuePair<string, string>("cc", model.cc),
                    new KeyValuePair<string, string>("subject", model.subject),
                    new KeyValuePair<string,string>("text", model.text)

                });

                //var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                return await client.PostAsync(_mailGun.RequestUri, content).ConfigureAwait(false);
            }
        }

        //public async Task<HttpResponseMessage> SendEmailAsync(Email model)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("api" + ":" + API_KEY)));


        //    var form = new Dictionary<string, string>();
        //    form["from"] = "from@example.com";
        //    form["to"] = "to@example.org";
        //    form["subject"] = "Test";
        //    form["text"] = "testing testing...";

        //    var response = await client.PostAsync("https://api.mailgun.net/v2/" + DOMAIN + "/messages", new FormUrlEncodedContent(form));

        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        Debug.WriteLine("Success");
        //    }
        //    else
        //    {
        //        Debug.WriteLine("StatusCode: " + response.StatusCode);
        //        Debug.WriteLine("ReasonPhrase: " + response.ReasonPhrase);
        //    }
        //}
    }
}
