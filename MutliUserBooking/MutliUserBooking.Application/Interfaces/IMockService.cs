using MutliUserBooking.Domain.Entities;
using System.Collections.Generic;

namespace MutliUserBooking.Application.Interfaces
{
    public interface IMockService
    {
        List<User> SeedUsers(int rowCount);

        List<Trip> SeedTrips(int rowCount);

        List<Position> GetPositions(int rowCount);
        
        List<Employee> GetEmployees(int rowCount);

        List<Position> SeedPositions(int rowCount);
    }
}