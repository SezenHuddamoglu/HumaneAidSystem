using CleanArchitecture.Core.Interfaces;
using System;

namespace CleanArchitecture.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
