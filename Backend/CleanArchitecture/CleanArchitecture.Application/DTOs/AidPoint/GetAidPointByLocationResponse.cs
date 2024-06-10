namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.DTOs.AidPoint
{
    public class GetAidPointByLocationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status{ get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

    }
}

