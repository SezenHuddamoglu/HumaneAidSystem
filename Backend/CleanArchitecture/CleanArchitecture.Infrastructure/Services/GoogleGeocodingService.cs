using System;
using System.Net.Http;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CleanArchitecture.Infrastructure.Services
{
    public class GoogleGeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleGeocodingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"];
        }

        public async Task<AidPoint> GetCoordinatesAsync(string address)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            if (json["status"].ToString() != "OK")
            {
                throw new Exception("Address not found");
            }

            var location = json["results"][0]["geometry"]["location"];
            var aidPoint = new AidPoint
            {
                Latitude = (double)location["lat"],
                Longitude = (double)location["lng"]
            };

            return aidPoint;
        }
    }
}
