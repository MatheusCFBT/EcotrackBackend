using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using EcotrackBusiness.Validations;

namespace EcotrackBusiness.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        public async Task Adicionar(Cliente cliente)
        {
            if(!ExecutarValidacao(new ClienteValidation(), cliente)) return; 
        }
    }
}