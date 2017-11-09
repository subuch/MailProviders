using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailProviders.Factory
{
    public enum EmailEnum
    {
        MailGun=0,
        SendGrid=1,
        Yahoo=2,
        Gmail=3,
        HotMail=4
        //Other Email Providers
    }
}
