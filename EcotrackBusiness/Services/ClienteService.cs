using System.Text.RegularExpressions;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using EcotrackBusiness.Validations;

namespace EcotrackBusiness.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IRepository<Cliente> _repository;

        public ClienteService(IRepository<Cliente> repository, INotificador notificador) : base(notificador)
        {
            _repository = repository;
        }

        public async Task Adicionar(Cliente cliente)
        {
            if(!ExecutarValidacao(new ClienteValidation(), cliente)) return; 

            if(_repository.Buscar(c => c.Cpf == cliente.Cpf).Result.Any())
            {
                Notificar("Já existe um usúario com esse CPF informado");
                return;
            }

            await _repository.Adicionar(cliente);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}