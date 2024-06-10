using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Enums;

namespace CleanArchitecture.Core.DTOs.AidRequest
{
    public class UpdateAidRequestStatusRequest
    {
        public int Id { get; set; }
        public AidRequestStatus Status { get; set; }  // Changed from string to AidRequestStatus enum
    }
}

