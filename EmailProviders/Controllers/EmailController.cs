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
        EmailHelper _helper;
        public EmailController()
        {
            _helper = new EmailHelper(); //IOC can be used for loose coupling
            _providers= _helper.getRandomProviders(); 
        }


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync([FromBody]Email datamodel)
        {

           
            
                      
            foreach (var item in _providers) //Iterating with the pool of providers
            { 
              
              
                try
                {
                    await  _helper.SendEmailHelper(item, datamodel);
                   return Ok("Mail Sent Succefully"); //Return the response if email sent successfull
                    
                }
                catch
                {
                    continue;    //pick the next provider if an error occurs with a provider
                }
            }

            return NotFound(DefaultConstant.NotFoundErrorMessage);

        }
        

    }
}
