using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CleanArchitecture.Core.DTOs.AidRequest;
using CleanArchitecture.Core.Interfaces;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AidRequestController : ControllerBase
    {
        private readonly IAidRequestService _aidRequestService;

        public AidRequestController(IAidRequestService aidRequestService)
        {
            _aidRequestService = aidRequestService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "DisasterAffected")]
        public async Task<IActionResult> AddAidRequest(AddAidRequestRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Kullanıcı kimliğini alır
            var result = await _aidRequestService.AddAidRequestAsync(request, userId);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAidRequestStatus(UpdateAidRequestStatusRequest request)
        {
            var result = await _aidRequestService.UpdateAidRequestStatusAsync(request);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

