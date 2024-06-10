using System.Collections.Generic;

namespace CleanArchitecture.Core.DTOs.AidRequest
{
    public class AddAidRequestRequest
    {
        public List<ProductDto> Products { get; set; }
        public string AidPointName { get; set; }
    }

    public class ProductDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
