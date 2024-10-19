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
            // Adiciona uma instancia da interface Repositorio
            _repository = repository;
        }

        public async Task<bool> Adicionar(Cliente cliente)
        {
            // Faz uma validacao da entidade Cliente
            if(!ExecutarValidacao(new ClienteValidation(), cliente)) return false; 

            // Verifica se o cpf do cliente ja esta no cadastrado no Db
            if(_repository.Buscar(c => c.Cpf == cliente.Cpf).Result.Any())
            {
                Notificar("Já existe um usúario com esse CPF informado");
                return false;
            }

            // Adiciona o cliente no Db se passar em todas as validacoes
            await _repository.Adicionar(cliente);

            return true;
        }

        public void Dispose()
        {
            // Libera recursos nao gerenciados quando nao forem mais necessarios
            _repository.Dispose();
        }
    }
}