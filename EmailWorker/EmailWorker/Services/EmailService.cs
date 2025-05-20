using EmailWorker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailWorker.Services
{
    public class EmailService
    {
        const string destiny = "aletestecode@gmail.com";
        const string title = "Teste de envio de email";
        public static string SendEmail(EmailServerAccount account)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(account.EmailOrigem, account.NomeOrigem);

            mailMessage.To.Add(new MailAddress(destiny));

            mailMessage.Subject = title;
            mailMessage.Body = "Teste de envio de email";
            mailMessage.IsBodyHtml = false;
            mailMessage.Priority = MailPriority.Normal;

            SmtpClient smtpClient = new SmtpClient(account.Server, account.Port);
            smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpClient.EnableSsl = (bool)account.Autentica;
            smtpClient.Host = account.Server;

            smtpClient.Credentials = new NetworkCredential(account.EmailOrigem, account.Pass);

            try
            {
                smtpClient.Send(mailMessage);
                return "Email enviado com sucesso!";
            }
            catch (Exception ex)
            {
                return "erro";
            }
        }
    }
}
