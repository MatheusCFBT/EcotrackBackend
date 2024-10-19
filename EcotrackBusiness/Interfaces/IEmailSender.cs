namespace EcotrackBusiness.Interfaces
{
    public interface IEmailSender
    {
        // Envia email
        Task<bool> EnviarEmail(string email, string token);
    }
}