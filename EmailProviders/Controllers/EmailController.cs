using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmailProviders.Factory;
using EmailProviders.Helper;
using EmailProviders.Models;
using EmailProviders.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmailProviders.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        IEnumerable<EmailEnum> _providers;
        public EmailController()
        {
            _providers= getRandomProviders();
        }


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody]Email datamodel)
        {

            if ( string.IsNullOrEmpty(datamodel.to) || string.IsNullOrEmpty(datamodel.bcc) || string.IsNullOrEmpty(datamodel.cc) || string.IsNullOrWhiteSpace(datamodel?.subject) || string.IsNullOrWhiteSpace(datamodel?.text))
            {
                return BadRequest(DefaultConstant.BadRequestErrorMessage);
            }
            
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            string responseData = string.Empty;            
            foreach (var item in _providers) //Iterating with the pool of providers
            { 
              
              
                try
                {
                    IEmailProvider emailProdvider =  EmailFactory.getEmailInstance((EmailEnum)Enum.Parse(typeof(EmailEnum), item.ToString()));
                    
                            responseMessage = await emailProdvider.SendEmailAsync(datamodel);
                    
                            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                using (HttpContent rescontent = responseMessage.Content)
                                {
                                    responseData = await rescontent.ReadAsStringAsync();
                                }                               
                                throw new Exception(responseData);
                            }

                        return Ok("Mail Sent Succefully"); //Return the response if email sent successfull
                    
                }
                catch
                {
                    continue;    //pick the next provider if an error occurs with a provider
                }
            }

            return NotFound(DefaultConstant.NotFoundErrorMessage);

        }

        //[HttpPost("SendEmail")]
        //public async Task<IActionResult> SendEmailWithArray([FromBody]Email datamodel)
        //{

        //    if (datamodel.tos.ToList().Count == 0 || datamodel.bccs.ToList().Count == 0 || datamodel.ccs.ToList().Count == 0 || string.IsNullOrWhiteSpace(datamodel?.subject) || string.IsNullOrWhiteSpace(datamodel?.text))
        //    {
        //        return BadRequest(DefaultConstant.BadRequestErrorMessage);
        //    }



        //    HttpResponseMessage responseMessage = new HttpResponseMessage();
        //    string responseData = string.Empty;
        //    int index = 0;
        //    foreach (var item in _providers) //Iterating with the pool of providers
        //    {
        //        var listWithMost = ((new List<List<string>> { datamodel.tos.ToList(), datamodel.bccs.ToList(), datamodel.ccs.ToList() })
        //                           .OrderByDescending(x => x.Count())
        //                           .Take(1)).ToList();

        //        try
        //        {
        //            IEmailProvider emailProdvider = EmailFactory.getEmailInstance((EmailEnum)Enum.Parse(typeof(EmailEnum), item.ToString()));

        //            for (int i = 0; i < listWithMost[0].Count; i++)
        //            {
        //                if (i >= index)
        //                {
        //                    if (datamodel.tos.ElementAtOrDefault(i) != null)
        //                        datamodel.to = datamodel.tos[i].ToString();
        //                    else
        //                        datamodel.to = "";

        //                    if (datamodel.bccs.ElementAtOrDefault(i) != null)
        //                        datamodel.bcc = datamodel.bccs[i].ToString();
        //                    else
        //                        datamodel.bcc = "";


        //                    if (datamodel.ccs.ElementAtOrDefault(i) != null)
        //                        datamodel.cc = datamodel.ccs[i].ToString();
        //                    else
        //                        datamodel.cc = "";

        //                    responseMessage = await emailProdvider.SendEmailAsync(datamodel);
        //                    index = i;
        //                    if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
        //                    {
        //                        using (HttpContent rescontent = responseMessage.Content)
        //                        {
        //                            responseData = await rescontent.ReadAsStringAsync();
        //                        }
        //                        throw new Exception(responseData);
        //                    }

        //                }
        //                //Return the response if email sent successfull
        //            }
        //            return Ok("Mail Sent Succefully");
        //        }
        //        catch
        //        {
        //            continue;    //pick the next provider if an error occurs with a provider
        //        }
        //    }

        //    return NotFound(DefaultConstant.NotFoundErrorMessage);

        //}


        private IEnumerable<EmailEnum> getRandomProviders()
        {
            List<EmailEnum> email = new List<EmailEnum>();
            email.AddRange(Enum.GetValues(typeof(EmailEnum)).Cast<EmailEnum>());
            Random rand = new Random();

            var randomizedList = (from item in email
                                 orderby rand.Next()
                                 select item).ToList<EmailEnum>();

            return randomizedList;
        }
    }
}
