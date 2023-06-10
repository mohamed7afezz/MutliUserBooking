using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Domain.Entities;
using MutliUserBooking.Infrastructure.Shared.Mock;
using System.Collections.Generic;

namespace MutliUserBooking.Infrastructure.Shared.Services
{
    public class MockService : IMockService
    {

        /// <summary>
        /// Generates a list of seed users using the UserBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of seed users to generate.</param>
        /// <returns>A list of seed users.</returns>
        public List<User> SeedUsers(int rowCount)
        {
            var faker = new UserBogusConfig();
            return faker.Generate(rowCount);
        }


        /// <summary>
        /// Generates a list of seed trips using the TripBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of seed trips to generate.</param>
        /// <returns>A list of seed users.</returns>
        public List<Trip> SeedTrips(int rowCount)
        {
            var faker = new TripBogusConfig();
            return faker.Generate(rowCount);
        }

    }
}