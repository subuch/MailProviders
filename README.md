# MailProviders
 This application is used to send email from the pool of MailProvider
 
 On filling the required fields, the system would randomly pick a provider to send email. During the failure, the system would automatically recognize it and divert the traffic to another provider from the pool without any interruption
 
 Pool of Techniques Used:
 .NET Core 2.0
 .NET Core WebAPI 2.0 
 Angular 1.x
 Single Page Application(SPA)
 
 Integrated Mail provider
 1. MailGun
 2. SendGrid
 
  
Architecture design based on the following:
 Singleton and Factory Pattern
 
 List of task before running the app:
 
 1. Create an account in SendGrid and note it down the UserId and Password
 2. Create an account in Mailgun and note it down the Key
 3. Ensure the recipients are addded in the MailGun account under the Authorized recipients section
 4. Open the appsetting.json(within the codebase) and modify the following with settings  provided by Mailgun and SendGrid
 
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

Info to Reviewer:
1. App support email to multiple  recipient provided each recipient is added in To,CC and BCC
2 It need version upgrade to integrate multiple recipient in To,CC and BCC
