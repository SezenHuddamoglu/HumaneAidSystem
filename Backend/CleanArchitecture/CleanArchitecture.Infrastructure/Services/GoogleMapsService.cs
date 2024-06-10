using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    internal class AidPointService
    {
        private readonly IGeocodingService _geocodingService;
        private readonly IRoutingService _routingService;
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public AidPointService(IGeocodingService geocodingService, IRoutingService routingService, IAidPointRepositoryAsync aidPointRepository)
        {
            _geocodingService = geocodingService;
            _routingService = routingService;
            _aidPointRepository = aidPointRepository;
        }
    }
}
