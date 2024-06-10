using CleanArchitecture.Core.DTOs.AidOffer;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AidOfferController : ControllerBase
    {
        private readonly IAidOfferService _aidOfferService;

        public AidOfferController(IAidOfferService aidOfferService)
        {
            _aidOfferService = aidOfferService;
        }

        [HttpPost("make-donation")]
        [Authorize(Roles = "Helper")]
        public async Task<IActionResult> MakeDonation(MakeDonationRequest request)
        {
            var user = HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                // Kullanıcının rollerini al
                var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                Console.WriteLine("User roles: " + string.Join(", ", roles));

                // Rol kontrolü yap
                if (!roles.Contains("Helper"))
                {
                    // Rol kontrolü başarısız olduğunda istenmeyen erişim hatası fırlat
                    Console.WriteLine("Unauthorized access attempt by user with roles: " + string.Join(", ", roles));
                    throw new UnauthorizedAccessException("Only users with the Helper role can make donations.");
                }
            }

            // Yetkilendirme başarılı ise devam et
            var result = await _aidOfferService.MakeDonationAsync(request);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
