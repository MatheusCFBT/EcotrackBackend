using Ecotrack.Context;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace EcotrackData.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(EcotrackDbContext dbContext) : base(dbContext) { }

        public async Task<Cliente> ObterClientePorCpf(string cpf)
        {
            return await ecotrackDb.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente> ObterClientePorEmail(string email)
        {
            return await ecotrackDb.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}