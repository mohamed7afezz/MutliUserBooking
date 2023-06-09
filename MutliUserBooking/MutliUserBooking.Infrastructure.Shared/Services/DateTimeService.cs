using MutliUserBooking.Application.Interfaces;
using System;

namespace MutliUserBooking.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}