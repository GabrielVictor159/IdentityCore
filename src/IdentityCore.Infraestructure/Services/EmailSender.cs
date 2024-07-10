using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using IdentityCore.Areas.Identity.Data;

namespace IdentityCore.Services;

public interface IMyEmailSender
{
    void SendEmail(string email, string subject, string HtmlMessage);
}

public class MyEmailSender : IMyEmailSender
{
    public void SendEmail(string email, string subject, string HtmlMessage)
    {
        using (MailMessage mm = new MailMessage(Environment.GetEnvironmentVariable("SMTP_USERNAME")!, email))
        {
            mm.Subject = subject;
            string body = HtmlMessage;
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Environment.GetEnvironmentVariable("SMTP_SERVER")!;
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(Environment.GetEnvironmentVariable("SMTP_USERNAME"), Environment.GetEnvironmentVariable("SMTP_PASSWORD"));
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!);
            smtp.Send(mm);
        }
    }
}
