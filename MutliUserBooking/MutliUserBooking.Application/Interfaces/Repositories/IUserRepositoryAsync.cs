using MutliUserBooking.Application.Features.User.Queries.GetUsers;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged user response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface IUserRepositoryAsync : IGenericRepositoryAsync<User>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedUserResponseAsync(GetUsersQuery requestParameters);
    }
}