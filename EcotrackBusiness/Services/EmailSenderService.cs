using System.Runtime.CompilerServices;
using EcotrackBusiness.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EcotrackBusiness.Services
{
    public class EmailSenderService : BaseService, IEmailSender
    {
        // Faz a instancia do notificador
        public EmailSenderService(INotificador notificador) : base(notificador)
        {
        }

        public async Task<bool> EnviarEmail(string email, string token)
        {
            // Instancia a Key da api para enviar emails
            // use a essa Key para enviar emails: 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

            string sendGridK = "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";

            // Faz integracao com a api para enviar para o cliente certo
            var client = new SendGridClient(sendGridK);

            // Endereco de email de quem enviara o email 
            var from = new EmailAddress("suporte.ecotrack@gmail.com", "Ecotrack");
            
            // Endereco de email de quem recebera o email 
            var to = new EmailAddress(email);

            string resetPasswordUrl = "http://127.0.0.1:5500/redefinição.html";
            // Assunto do email
            string subject = "Recuperação de senha - Ecotrack";

            // Mensagem que contem no email
            string plainTextContent = $"Olá,\n\nVocê solicitou a recuperação de sua senha.\n\n" +
                                      $"Por favor, copie o seguinte token para continuar o processo de recuperação:\n\n" +
                                      $"Token: {token}\n\n" +
                                      $"Depois, clique no link abaixo para redefinir sua senha:\n" +
                                      $"{resetPasswordUrl}\n\n" +
                                      $"Se você não solicitou essa recuperação, por favor ignore este e-mail.\n\n" +
                                      $"Atenciosamente,\nEquipe Ecotrack";

            // Mensagem que contem no email com codigo HTML e CSS para deixar mais elegante
            string htmlContent = $"<p>Olá,</p>" +
                                            $"<p>Você solicitou a recuperação de sua senha.</p>" +
                                            $"<p><strong>Por favor, copie o seguinte token para continuar o processo de recuperação:</strong></p>" +
                                            $"<p style=\"color: #2e6da4;\"><strong>Token: {token}</strong></p>" +
                                            $"<p>Depois, clique no link abaixo para redefinir sua senha:</p>" +
                                            $"<p><a href=\"{resetPasswordUrl}\" style=\"background-color: #2e6da4; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;\">Redefinir Senha</a></p>" +
                                            $"<p>Se você não solicitou essa recuperação, por favor ignore este e-mail.</p>" +
                                            $"<p>Atenciosamente,<br>Equipe Ecotrack</p>";

            // Cria o email que será enviado
            var Email = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Envia o email
            var EmailSender = await client.SendEmailAsync(Email);

            // Verifica se o email foi enviado
            if (EmailSender.IsSuccessStatusCode)
            {
                return true;
            }

            // Obtem o erro e passa para o notificador
            var errorMessage = await EmailSender.Body.ReadAsStringAsync();
            Notificar(errorMessage);

            return false;        
        }
    }
}