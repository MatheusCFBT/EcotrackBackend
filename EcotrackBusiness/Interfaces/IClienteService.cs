using EcotrackBusiness.Models;

namespace EcotrackBusiness.Interfaces
{
    public interface IClienteService
    {
        Task Adicionar(Cliente cliente);
    }
}