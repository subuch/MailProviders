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

            if (  datamodel.tos.Count()==0 || datamodel.bccs.Count() == 0 || datamodel.ccs.Count() == 0   || string.IsNullOrWhiteSpace(datamodel?.subject) || string.IsNullOrWhiteSpace(datamodel?.text))
            {
                return BadRequest(DefaultConstant.BadRequestErrorMessage);
            }

          

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            string responseData = string.Empty;
            int index = 0;
            foreach (var item in _providers) //Iterating with the pool of providers
            {

                //if (isErrorThrown)
                //{
                //    var listWithMost = (new List<List<string>> { datamodel.tos.ToList(), datamodel.bccs.ToList(), datamodel.ccs.ToList() })
                //               .OrderByDescending(x => x.Count())
                //               .Take(1);
                //}
                //else
                //{
                    var listWithMost = (new List<List<string>> { datamodel.tos.ToList(), datamodel.bccs.ToList(), datamodel.ccs.ToList() })
                                 .OrderByDescending(x => x.Count())
                                 .Take(1);
                //}
                try
                {
                    IEmailProvider emailProdvider =  EmailFactory.getEmailInstance((EmailEnum)Enum.Parse(typeof(EmailEnum), item.ToString()));

                    for (int i = 0; i < listWithMost.Count(); i++)
                    {
                        if (i >= index)
                        {
                            if (datamodel.tos[i] != null)
                                datamodel.to = datamodel.tos[i].ToString();

                            if (datamodel.bccs[i] != null)
                                datamodel.bcc = datamodel.bccs[i].ToString();

                            if (datamodel.ccs[i] != null)
                                datamodel.cc = datamodel.ccs[i].ToString();

                            responseMessage = await emailProdvider.SendEmailAsync(datamodel);
                            index = i;
                            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                using (HttpContent rescontent = responseMessage.Content)
                                {
                                    responseData = await rescontent.ReadAsStringAsync();
                                }                               
                                throw new Exception(responseData);
                            }
                           
                        }
                        //Return the response if email sent successfull
                    }
                    return Ok("Mail Sent Succefully");
                }
                catch
                {
                    continue;    //pick the next provider if an error occurs with a provider
                }
            }

            return NotFound(DefaultConstant.NotFoundErrorMessage);

        }


        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        /// <summary>
        /// Random list sorting
        /// </summary>
        /// <returns></returns>
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
