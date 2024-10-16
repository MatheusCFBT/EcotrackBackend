using EcotrackBusiness.Models;

namespace EcotrackBusiness.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClientePorCpf(Guid id);

        Task<Cliente> ObterClientePorEMail(Guid id);
    }
}