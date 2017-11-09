using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SendGridProvider : IEmailProvider
    {
        SendGridEmailSetting _mailGun;

        public SendGridProvider()
        {
            _mailGun = ConfigurationService.Instance.SendGridEmailSetting;

        }


        public async Task<HttpResponseMessage> SendEmailAsync(Email model)
        {
            string responseData = string.Empty;
            using (var client = new HttpClient { BaseAddress = new Uri(_mailGun.BaseUri) })
            {                
                var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("api_key", _mailGun.APIKey),
                    new KeyValuePair<string, string>("api_user", _mailGun.APIUser),
                    new KeyValuePair<string, string>("from", model.From),
                    new KeyValuePair<string, string>("to", model.to),
                     new KeyValuePair<string, string>("bcc", model.bcc),
                    new KeyValuePair<string, string>("cc", model.cc),
                    new KeyValuePair<string, string>("subject", model.subject),
                    new KeyValuePair<string,string>("text", model.text)

                });
                HttpResponseMessage response =  client.PostAsync("/api/mail.send.json", content).Result;
                //string resultContent = await response.Content.ReadAsStringAsync();
                //if (response.StatusCode != System.Net.HttpStatusCode.OK)
                //       throw new Exception(resultContent);
                //    return response;
                using (HttpContent rescontent = response.Content)
                {
                    responseData = await rescontent.ReadAsStringAsync();
                }
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(responseData);
                return response;
            }
        }

        //public async Task<HttpResponseMessage> SendEmailAsync(Email model)
        //{

        //    HttpClient httpClient = null;
        //    string responseData = string.Empty;
        //    try
        //    {
        //        using (var postData = new MultipartFormDataContent())
        //        {
        //            var subjectContent = new StringContent(model.subject);
        //            subjectContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = model.subject
        //            };
        //            postData.Add(subjectContent);

        //            var bodyContent = new StringContent(model.text);
        //            bodyContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = model.text
        //            };
        //            postData.Add(bodyContent);

        //            //setup api key and api user param
        //            var apiUserContent = new StringContent(_mailGun.APIUser);
        //            apiUserContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = _mailGun.APIUser
        //            };
        //            postData.Add(apiUserContent);
        //            var apiKeyContent = new StringContent(_mailGun.APIKey);
        //            apiKeyContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = _mailGun.APIKey
        //            };
        //            postData.Add(apiKeyContent);

        //            var fromContent = new StringContent(model.From);
        //            fromContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = model.From
        //            };
        //            postData.Add(fromContent);

        //            StringContent toContent = new StringContent(model.to);
        //            toContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = model.to
        //            };
        //            postData.Add(toContent);


        //            string fullUrl = string.Format("https://{0}{1}", _mailGun.RequestUri, _mailGun.BaseUri) ;
        //            httpClient = new HttpClient();
        //            HttpResponseMessage response = await httpClient.PostAsync(new Uri(fullUrl), postData);
        //            using (HttpContent content = response.Content)
        //            {
        //                responseData = await content.ReadAsStringAsync();
        //            }
        //            if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //                throw new Exception(responseData);
        //            return response;
        //        }
               
        //    }
        //    finally {
        //        if (httpClient != null)
        //            ((IDisposable)httpClient).Dispose();
        //    }
        //}
    }
}
