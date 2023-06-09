﻿using MutliUserBooking.Application.Features.Employees.Queries.GetEmployees;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged employee response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface IEmployeeRepositoryAsync : IGenericRepositoryAsync<Employee>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEmployeeResponseAsync(GetEmployeesQuery requestParameters);
    }
}