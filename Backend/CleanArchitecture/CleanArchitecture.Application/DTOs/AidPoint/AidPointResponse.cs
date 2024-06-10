namespace CleanArchitecture.Core.DTOs.AidPoint
{
    public class AidPointResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
      //  public string AidPointId { get; set; }
        public string Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
