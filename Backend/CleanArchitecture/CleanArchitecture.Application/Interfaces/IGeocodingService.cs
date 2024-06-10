using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IGeocodingService
    {
        Task<AidPoint> GetCoordinatesAsync(string address);
    }
}

   