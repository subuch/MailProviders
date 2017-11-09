using System.IO;
using EmailProviders.Service.Config;
using Microsoft.Extensions.Configuration;

namespace EmailProviders.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;
        private static ConfigurationService _instance;

        // Lock synchronization object
        private static readonly object SyncLock = new object();

        // Constructor is 'protected'
        //Implemented the Singleton Pattern
        protected ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public static ConfigurationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationService();
                        }
                    }
                }
                return _instance;
            }
        }
       
        public MailGunEmailSetting MailGunEmailSetting
        {
            get
            {
                var section = _configuration.GetSection("MailGunEmailSettings");
                return section.Get<MailGunEmailSetting>();
            }
        }

        public SendGridEmailSetting SendGridEmailSetting
        {
            get
            {
                var section = _configuration.GetSection("SendGridMailSettings");
                return section.Get<SendGridEmailSetting>();
            }
        }
    }
}
