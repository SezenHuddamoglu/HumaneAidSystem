using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class AidOfferRepositoryAsync : GenericRepositoryAsync<AidOffer>, IAidOfferRepositoryAsync
    {
        private readonly DbSet<AidOffer> _aidOffers;

        public AidOfferRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _aidOffers = dbContext.Set<AidOffer>();
        }

    }
}
