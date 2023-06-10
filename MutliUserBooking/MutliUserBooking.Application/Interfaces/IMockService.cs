using MutliUserBooking.Domain.Entities;
using System.Collections.Generic;

namespace MutliUserBooking.Application.Interfaces
{
    public interface IMockService
    {
        List<User> GetUsers(int rowCount);
        List<User> SeedUsers(int rowCount);
        List<Position> GetPositions(int rowCount);
        
        List<Employee> GetEmployees(int rowCount);

        List<Position> SeedPositions(int rowCount);
    }
}