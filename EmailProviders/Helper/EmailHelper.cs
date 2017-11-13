using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EmailProviders.Factory;
using EmailProviders.Models;

namespace EmailProviders.Helper
{
    public class EmailHelper
    {
        HttpResponseMessage responseMessage = new HttpResponseMessage();
        string responseData = string.Empty;

        public IEnumerable<EmailEnum> getRandomProviders()
        {
            List<EmailEnum> email = new List<EmailEnum>();
            email.AddRange(Enum.GetValues(typeof(EmailEnum)).Cast<EmailEnum>());
            Random rand = new Random();

            var randomizedList = (from item in email
                                  orderby rand.Next()
                                  select item).ToList<EmailEnum>();

            return randomizedList;
        }

        public async Task<HttpStatusCode> SendEmailHelper(object item, Email datamodel)
        {
            IEmailProvider emailProdvider = EmailFactory.getEmailInstance((EmailEnum)Enum.Parse(typeof(EmailEnum), item.ToString()));

            responseMessage = await emailProdvider.SendEmailAsync(datamodel);

            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                
                throw new Exception(responseData);
            }
            else
            { 
                return HttpStatusCode.OK;
            }

        }
    }
}
