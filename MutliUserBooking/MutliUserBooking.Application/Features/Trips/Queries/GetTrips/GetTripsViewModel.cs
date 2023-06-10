using MutliUserBooking.Domain.Enums;
using System;

namespace MutliUserBooking.Application.Features.Trips.Queries.GetTrips
{
    public class GetTripsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}