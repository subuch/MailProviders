using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailProviders.Models
{
    public class Email
    {
       
        public string From { get; set; }
        public string[] tos { get; set; }
        public string to { get; set; }
        public string[] ccs { get; set; }
        public string cc { get; set; }
        public string[] bccs { get; set; }
        public string bcc { get; set; }

        public string subject { get; set; }

        public string text { get; set; }

    }
}
