using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmailProviders.Models;

namespace EmailProviders.Helper
{
    public interface IEmailProvider
    {
       Task<HttpResponseMessage> SendEmailAsync(Email model);
    }
}
