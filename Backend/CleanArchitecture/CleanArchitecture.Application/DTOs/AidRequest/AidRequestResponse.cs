using CleanArchitecture.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.AidRequest
{
    public class AidRequestResponse
    {
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public string UserId { get; set; }
        public List<CleanArchitecture.Core.Entities.Product> Products { get; set; }

       // [SwaggerProperty("AidPointId")]
        public string AidPointId { get; set; } 
        public string AidPointName { get; set; }

        public AidRequestResponse(IAidPointRepositoryAsync aidPointRepository)
        {
            _aidPointRepository = aidPointRepository;
        }

       /* public async Task SetAidPointIdFromNameAsync()
        {
            AidPointId = await _aidPointRepository.GetAidPointIdByName(AidPointName);
        }*/

        public async Task SetAidPointIdFromNameAsync(IAidPointRepositoryAsync aidPointRepository)
        {
            AidPointId = await aidPointRepository.GetAidPointIdByName(AidPointName);
        }

    }
}


