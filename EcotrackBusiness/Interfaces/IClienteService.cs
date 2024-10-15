using EcotrackBusiness.Models;

namespace EcotrackBusiness.Interfaces
{
    public interface IClienteService : IDisposable
    {
        Task<bool> Adicionar(Cliente cliente);
    }
}