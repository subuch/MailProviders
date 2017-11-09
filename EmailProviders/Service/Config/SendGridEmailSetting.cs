using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailProviders.Service.Config
{
    public class SendGridEmailSetting
    {
        public string APIUser { get; set; }
        public string APIKey { get; set; }

        public string Key { get; set; }

        public string BaseUri { get; set; }
        public string RequestUri { get; set; }

        public string From { get; set; }
    }
}
