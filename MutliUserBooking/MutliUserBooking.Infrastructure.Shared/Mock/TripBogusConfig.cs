using AutoBogus;
using Bogus;
using MutliUserBooking.Domain.Entities;
using MutliUserBooking.Domain.Enums;
using System;

namespace MutliUserBooking.Infrastructure.Shared.Mock
{
    public class TripBogusConfig : AutoFaker<Trip>
    {
        public TripBogusConfig()
        {
            Randomizer.Seed = new Random(8675309);
            RuleFor(p => p.Id, f => Guid.NewGuid());
            RuleFor(p => p.Name, f => "Trip to: " + f.Address.Country());
            RuleFor(p => p.CityName, f => f.Address.City());
            RuleFor(p => p.Price, f => f.Random.Decimal(100,500));
            RuleFor(p => p.ImageUrl, f => "/images/trips/"+ Guid.NewGuid() + ".png");
            RuleFor(p => p.Content, f => f.Commerce.ProductDescription());
            RuleFor(p => p.CreationDate, f => f.Date.Past(1));
        }
    }
}