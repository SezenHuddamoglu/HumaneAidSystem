using System.Collections.Generic;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint
{
    public class SearchAidPointsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
       // public string AidPointId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
