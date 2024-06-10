using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class AidRequestRepositoryAsync : GenericRepositoryAsync<AidRequest>, IAidRequestRepositoryAsync
    {
        private readonly DbSet<AidRequest> _aidRequests;
        private readonly ApplicationDbContext _dbContext;

        public AidRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _aidRequests = dbContext.Set<AidRequest>();
            _dbContext = dbContext;
        }

        public async Task AddAsync(AidRequest aidRequest)
        {
            await _aidRequests.AddAsync(aidRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AidRequest> GetByIdAsync(int id)
        {
            return await _aidRequests.FindAsync(id);
        }

        public async Task UpdateAsync(AidRequest aidRequest)
        {
            _aidRequests.Update(aidRequest);
            await _dbContext.SaveChangesAsync();
        }
    }
}

