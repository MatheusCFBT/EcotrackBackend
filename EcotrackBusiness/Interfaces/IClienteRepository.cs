using EcotrackBusiness.Models;

namespace EcotrackBusiness.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClientePorCpf(string cpf);

        Task<Cliente> ObterClientePorEmail(string email);
    }
}