//using CleanArchitecture.Application.Entities;
using CleanArchitecture.Core.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities
{
    public class AidRequest : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public string ProductsJson { get; set; }

        [NotMapped]
        public List<Product> Products
        {
            get => string.IsNullOrEmpty(ProductsJson) ? new List<Product>() : JsonSerializer.Deserialize<List<Product>>(ProductsJson);
            set => ProductsJson = JsonSerializer.Serialize(value);
        }

        public AidRequestStatus Status { get; set; }
        public string Location { get; set; }
        public string AidPointId { get; set; }
        public string AidPointName { get; set; }

    }
}


/*using CleanArchitecture.Core.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities
{
    public class AidRequest : AuditableBaseEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AidRequest(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public List<Product> Products { get; internal set; }
        public AidRequestStatus Status { get; internal set; }
        public string Description { get; internal set; }
        public string Name { get; internal set; }
        public string Location { get; internal set; } // Location eklendi
        public int AidPointId { get; internal set; }
    }
}*/

