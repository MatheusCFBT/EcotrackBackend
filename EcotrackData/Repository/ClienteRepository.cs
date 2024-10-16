using Ecotrack.Context;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace EcotrackData.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(EcotrackDbContext dbContext) : base(dbContext) { }

        public async Task<Cliente> ObterClientePorCpf(Guid id)
        {
            return await ecotrackDb.Clientes.AsNoTracking().Include(c => c.Cpf)
                .FirstOrDefaultAsync();
        }

        public async Task<Cliente> ObterClientePorEMail(Guid id)
        {
            return await ecotrackDb.Clientes.AsNoTracking().Include(c => c.Email)
                .FirstOrDefaultAsync();
        }
    }
}