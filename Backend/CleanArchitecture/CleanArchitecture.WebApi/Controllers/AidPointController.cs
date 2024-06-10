using CleanArchitecture.Core.DTOs.AidPoint;
using CleanArchitecture.Core.Interfaces;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AidPointController : ControllerBase
    {
        private readonly IAidPointService _aidPointService;
        private readonly AidPointService _AidPointService;// route için

        public AidPointController(IAidPointService aidPointService)
        {
            _aidPointService = aidPointService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAidPoint(AddAidPointRequest request)
        {
            var result = await _aidPointService.AddAidPointAsync(request);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /*  [HttpDelete("remove/{id}")]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> RemoveAidPoint(string id)
          {
              var result = await _aidPointService.RemoveAidPointAsync(id);
              if (result.Succeeded)
              {
                  return Ok(result);
              }
              return BadRequest(result);
          }

          [HttpPut("update-status/{id}")]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> UpdateAidPointStatus(string id, UpdateAidPointStatusRequest request)
          {
              var result = await _aidPointService.UpdateAidPointStatusAsync(id, request);
              if (result.Succeeded)
              {
                  return Ok(result);
              }
              return BadRequest(result);
          }
        */
        [HttpDelete("remove/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAidPoint(string id)
        {
            object parsedId = ParseId(id);
            var result = await _aidPointService.RemoveAidPointAsync(parsedId);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update-status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAidPointStatus(string id, UpdateAidPointStatusRequest request)
        {
            object parsedId = ParseId(id);
            var result = await _aidPointService.UpdateAidPointStatusAsync(parsedId, request);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        private object ParseId(string id)
        {
            if (int.TryParse(id, out int intId))
            {
                return intId;
            }
            return id;
        }
        [HttpGet("location")]
        public async Task<IActionResult> GetAidPointByLocation([FromQuery] string location)
        {
            var result = await _aidPointService.GetAidPointByLocationAsync(location);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAidPoints([FromQuery] SearchAidPointsRequest request)
        {
            var result = await _aidPointService.SearchAidPointsAsync(request);
            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _aidPointService.GetAllAidPointsAsync();
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }


        //route http'si
        [HttpGet("route")]
        public async Task<IActionResult> GetRoute([FromQuery] AidPoint origin, [FromQuery] AidPoint destination)
        {

            try
            {
                var route = await _AidPointService.GetRouteAsync(origin, destination);
                return Ok(route);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //API endpoint
        [HttpGet]
        public async Task<ActionResult<List<AidPoint>>> GetAidPoints()
        {
            var aidPoints = await _aidPointService.GetAllAidPointsAsync();
            return Ok(aidPoints);
        }
    }
}