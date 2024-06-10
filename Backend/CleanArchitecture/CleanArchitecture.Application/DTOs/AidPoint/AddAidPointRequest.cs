namespace CleanArchitecture.Core.DTOs.AidPoint
{
    public class AddAidPointRequest
    {
        public string Name { get; set; }
       // public string AidPointId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public double Latitude { get; set; } // Enlem
        public double Longitude { get; set; } // Boylam
    }
}

