using System.Text.RegularExpressions;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using EcotrackBusiness.Validations;

namespace EcotrackBusiness.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository, INotificador notificador) : base(notificador)
        {
            _repository = repository;
        }

        public async Task<bool> Adicionar(Cliente cliente)
        {
            if(!ExecutarValidacao(new ClienteValidation(), cliente)) return false; 

            if(_repository.Buscar(c => c.Cpf == cliente.Cpf).Result.Any())
            {
                Notificar("Já existe um usúario com esse CPF informado");
                return false;
            }

            await _repository.Adicionar(cliente);

            return true;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}