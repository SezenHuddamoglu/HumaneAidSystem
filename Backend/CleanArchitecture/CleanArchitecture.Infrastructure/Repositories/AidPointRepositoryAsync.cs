using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class AidPointRepositoryAsync : GenericRepositoryAsync<AidPoint>, IAidPointRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public AidPointRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetAidPointIdByName(string aidPointName)
        {
            var aidPoint = await _dbContext.AidPoints.FirstOrDefaultAsync(ap => ap.Name == aidPointName);
            return aidPoint?.Id.ToString(); 
        }


        public async Task<GetAidPointByLocationResponse> GetByLocationAsync(string location)
        {
            var aidPoint = await _dbContext.AidPoints
                .Where(ap => ap.Location == location)
                .FirstOrDefaultAsync();

            if (aidPoint == null)
            {
                return null;
            }

            return new GetAidPointByLocationResponse
            {
                Id = aidPoint.Id,
                Name = aidPoint.Name,
                Location = aidPoint.Location,
                Status = aidPoint.Status
            };
        }

        public Task GetByLocationAsync(string latitude, object longitude)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AidPoint>> SearchAsync(string searchTerm)
        {
            if (searchTerm == null)
            {
                
                throw new ArgumentNullException(nameof(searchTerm), "Search term cannot be null.");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            return await _dbContext.AidPoints
                .Where(ap => ap.Name.ToLower().Contains(lowerCaseSearchTerm) ||
                             ap.Location.ToLower().Contains(lowerCaseSearchTerm) ||
                             ap.Status.ToLower().Contains(lowerCaseSearchTerm) ||
                             //ap.AidPointId.ToLower().Contains(lowerCaseSearchTerm) ||
                             ap.Latitude.ToString().Contains(lowerCaseSearchTerm) ||
                             ap.Longitude.ToString().Contains(lowerCaseSearchTerm))
                .ToListAsync();
        }
        public async Task<IEnumerable<AidPoint>> GetAllAsync()
        {
            return await _dbContext.AidPoints.ToListAsync();
        }


    }
}
