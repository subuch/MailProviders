# MailProviders
 This application is used to send email from the pool of MailProvider
 
 If an error occurs while sending with a provider, system would automatically recogonise it and divert the traffic to another provider from the pool.
 
 Pool of Technique Used:
 .NET Core 2.0
 .NET Core WebAPI 2.0
 Angular 1.x
 Single Page Application(SPA)
 
 Integrated Mail provider
 1. MailGun
 2. SendGrid
 
  
Architecture design based on the following:
 Singleton
 Factory Pattern
 
 Config to modified:
 
 Open the appsetting.json and modify the following with the own API Keys provided by Mailgun and SendGrid
 
  "MailGunEmailSettings": {
    "ApiKey": "api:key-xxxx", 
    "BaseUri": "https://api.mailgun.net/v3/",
    "RequestUri": "sandboxexxxx.mailgun.org/messages",
    "From": "postmaster@sandboxexxxx.mailgun.org"
  },

  "SendGridMailSettings": {
    "APIUser": "xxxx",
    "APIKey": "xxxx",
    "Key": "api:key-xxxx",
    "BaseUri": "https://api.sendgrid.com/v3/mail/send"
  
  }

