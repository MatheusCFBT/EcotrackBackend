using System.Runtime.CompilerServices;
using EcotrackBusiness.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EcotrackBusiness.Services
{
    public class EmailSenderService : IEmailSender
    {
        public async Task<bool> EnviarEmail(string email)
        {
            string apiKey = "ecotrackEMail";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("suporte.ecotrack@gmail.com", "Ecotrack");
            var to = new EmailAddress(email);

            string subject = "Recuperação de senha - Ecotrack";
            string plainTextContent = "Olá, você requisitou a recuperação de senha. Clique no link abaixo para redefinir sua senha ";
            string htmlContent ="<p>Olá, você requisitou a recuperação de senha. Clique no link abaixo para redefinir sua senha</p>";

            var sendEmail = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(sendEmail);

            return true;
        }
    }
}