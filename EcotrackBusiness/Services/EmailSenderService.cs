using System.Runtime.CompilerServices;
using EcotrackBusiness.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EcotrackBusiness.Services
{
    public class EmailSenderService : IEmailSender
    {
        public async Task<bool> EnviarEmail(string email, string token)
        {
            string apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("suporte.ecotrack@gmail.com", "Ecotrack");
            var to = new EmailAddress(email);

            string subject = "Recuperação de senha - Ecotrack";
            string plainTextContent = $"Olá,\n\nVocê solicitou a recuperação de sua senha.\n\n" +
                                      $"Por favor, copie o seguinte token para continuar o processo de recuperação:\n\n" +
                                      $"Token: {token}\n\n" +
                                      $"Depois, clique no link abaixo para redefinir sua senha:\n" +
                                      "{resetPasswordUrl}\n\n" +
                                      $"Se você não solicitou essa recuperação, por favor ignore este e-mail.\n\n" +
                                      $"Atenciosamente,\nEquipe Ecotrack";

            string htmlContent = $"<p>Olá,</p>" +
                                            $"<p>Você solicitou a recuperação de sua senha.</p>" +
                                            $"<p><strong>Por favor, copie o seguinte token para continuar o processo de recuperação:</strong></p>" +
                                            $"<p style=\"color: #2e6da4;\"><strong>Token: {token}</strong></p>" +
                                            $"<p>Depois, clique no link abaixo para redefinir sua senha:</p>" +
                                            "<p><a href=\"{resetPasswordUrl}\" style=\"background-color: #2e6da4; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;\">Redefinir Senha</a></p>" +
                                            $"<p>Se você não solicitou essa recuperação, por favor ignore este e-mail.</p>" +
                                            $"<p>Atenciosamente,<br>Equipe Ecotrack</p>";

            var sendEmail = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(sendEmail);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            // Logar a resposta para verificar o erro
            var errorMessage = await response.Body.ReadAsStringAsync();
            Console.WriteLine($"Erro ao enviar o e-mail: {errorMessage}");

            return false;        
        }
    }
}