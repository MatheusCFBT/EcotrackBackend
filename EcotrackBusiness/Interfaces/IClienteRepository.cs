using EcotrackBusiness.Models;

namespace EcotrackBusiness.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        // Obtem o Cliente pelo cpf
        Task<Cliente> ObterClientePorCpf(string cpf);

        // Obtem o Cliente pelo email
        Task<Cliente> ObterClientePorEmail(string email);
    }
}