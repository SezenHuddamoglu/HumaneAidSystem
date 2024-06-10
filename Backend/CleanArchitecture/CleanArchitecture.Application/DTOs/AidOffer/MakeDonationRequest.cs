using CleanArchitecture.Core.DTOs.Product;
using CleanArchitecture.Core.DTOs.Products;
using CleanArchitecture.Core.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Enums;
using System.Collections.Generic;
using CoreProduct = CleanArchitecture.Core.Entities.Product;

namespace CleanArchitecture.Core.DTOs.AidOffer
{
    public class MakeDonationRequest
    {
        //public string Category { get; set; }
        public List<CoreProduct> Products { get; set; }
        //public decimal Amount { get; set; }
       
    }
}

