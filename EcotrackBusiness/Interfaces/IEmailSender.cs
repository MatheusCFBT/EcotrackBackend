namespace EcotrackBusiness.Interfaces
{
    public interface IEmailSender
    {
            Task<bool> EnviarEmail(string email);
    }
}