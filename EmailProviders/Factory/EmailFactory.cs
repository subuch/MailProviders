using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailProviders.Helper;

namespace EmailProviders.Factory
{
    /// <summary>
    /// Implementing Factory pattern
    /// </summary>
    public class EmailFactory
    {
        public static IEmailProvider getEmailInstance(EmailEnum emailProvider)
        {
            IEmailProvider _emailProvider;
            switch (emailProvider.ToString())
            {
                case "MailGun":
                    _emailProvider = new MailGunProvider();
                    break;
                case "SendGrid":
                    _emailProvider = new SendGridProvider();
                    break;
                default:
                    _emailProvider = new SendGridProvider();
                    break;
            }
            return _emailProvider;
        }
    }
}
