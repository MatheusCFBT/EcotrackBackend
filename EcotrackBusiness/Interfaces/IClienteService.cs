using EcotrackBusiness.Models;

namespace EcotrackBusiness.Interfaces
{
    public interface IClienteService : IDisposable
    {
        // Adiciona o cliente no Db
        Task<bool> Adicionar(Cliente cliente);
    }
}