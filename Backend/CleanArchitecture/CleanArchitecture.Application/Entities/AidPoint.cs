using CleanArchitecture.Core.Entities;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities
{
    public class AidPoint : AuditableBaseEntity
    {
        //public bool IsActive { get;  set; }
        //public string AidPointId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public double Latitude { get; set; } // Enlem
        public double Longitude { get; set; } // Boylam
    }
}

