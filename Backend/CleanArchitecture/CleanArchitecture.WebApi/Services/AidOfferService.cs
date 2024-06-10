using CleanArchitecture.Core.DTOs.AidOffer;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public class AidOfferService : IAidOfferService
    {
        private readonly IAidOfferRepositoryAsync _aidOfferRepository;

        public AidOfferService(IAidOfferRepositoryAsync aidOfferRepository)
        {
            _aidOfferRepository = aidOfferRepository;
        }

        public async Task<Response<int>> MakeDonationAsync(MakeDonationRequest request)
        {
            var aidOffer = new AidOffer
            {
                //AidCategory = request.Category,
                Products = request.Products
            };

            try
            {
                await _aidOfferRepository.AddAsync(aidOffer);
                return new Response<int>(aidOffer.Id);
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                var errorMessage = $"An error occurred while making donation: {ex.Message}. Inner exception: {innerExceptionMessage}";
                return new Response<int>(errorMessage);
            }
        }
    }
}
