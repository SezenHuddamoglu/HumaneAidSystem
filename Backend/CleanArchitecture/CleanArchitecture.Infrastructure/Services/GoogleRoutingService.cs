using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CleanArchitecture.Core.Interfaces;
using global::HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Services

{
    public class GoogleRoutingService : IRoutingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleRoutingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"];
        }

        public async Task<string> GetRouteAsync(AidPoint origin, AidPoint destination)
        {
            var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin.Latitude},{origin.Longitude}&destination={destination.Latitude},{destination.Longitude}&key={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            if (json["status"].ToString() != "OK")
            {
                throw new Exception("Route not found");
            }

            return json["routes"][0]["overview_polyline"]["points"].ToString();
        }
    }

}