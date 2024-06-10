using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.DTOs.Products
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; } // Amount for each product
    }
}
