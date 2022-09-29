using System;
using Microsoft.Exchange.WebServices.Data;
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
            service.Credentials = new WebCredentials("YourMailLink", "YourPassword"); 
            //service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;
            service.AutodiscoverUrl("YourMailLink", RedirectionUrlValidationCallback);

            EmailMessage email = new EmailMessage(service);
            email.ToRecipients.Add("RecipientMailLink");
            email.Subject = "HelloWorld";
            email.Body = new MessageBody("This email sent by using the EWS Managed API");
            email.Send();
        }
        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }
    }
}