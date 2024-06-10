using CleanArchitecture.Core.DTOs.AidOffer;
using CleanArchitecture.Core.Wrappers;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IAidOfferService
    {
        Task<Response<int>> MakeDonationAsync(MakeDonationRequest request);
    }
}
