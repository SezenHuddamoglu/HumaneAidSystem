//using CleanArchitecture.Application.Entities;
using CleanArchitecture.Core.DTOs.Products;
using CleanArchitecture.Core.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities
{
    public class AidOffer : AuditableBaseEntity
    {

        public string ProductsJson { get; set; }

        [NotMapped]
        public List<Product> Products
        {
            get => string.IsNullOrEmpty(ProductsJson) ? new List<Product>() : JsonSerializer.Deserialize<List<Product>>(ProductsJson);
            set => ProductsJson = JsonSerializer.Serialize(value);
        }

        public int UserId { get; set; }

    }
  

 
}
