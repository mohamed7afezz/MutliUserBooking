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

        /// <summary>
        /// Generates a list of seed positions using the PositionSeedBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of seed positions to generate.</param>
        /// <returns>A list of seed positions.</returns>
        public List<Position> SeedPositions(int rowCount)
        {
            var faker = new PositionSeedBogusConfig();
            return faker.Generate(rowCount);
        }

        /// <summary>
        /// Generates a list of positions using the PositionInsertBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of positions to generate.</param>
        /// <returns>A list of generated positions.</returns>
        public List<Position> GetPositions(int rowCount)
        {
            var faker = new PositionInsertBogusConfig();
            return faker.Generate(rowCount);
        }



        /// <summary>
        /// Gets a list of Employees using the EmployeeBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of Employees to generate.</param>
        /// <returns>A list of Employees.</returns>
        public List<Employee> GetEmployees(int rowCount)
        {
            var faker = new EmployeeBogusConfig();
            return faker.Generate(rowCount);
        }

    }
}