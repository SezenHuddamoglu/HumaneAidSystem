using System.Collections.Generic;

namespace CleanArchitecture.Core.DTOs.AidOffer
{
   
    public class AidOfferResponse
    {
        public string Category { get; set; }
        public List<CleanArchitecture.Core.Entities.Product> Products { get; set; }
        public double Amount { get; set; }
        
    }
}