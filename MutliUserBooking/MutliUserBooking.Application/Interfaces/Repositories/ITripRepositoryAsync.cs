using MutliUserBooking.Application.Features.Trips.Queries.GetTrips;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged trip response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface ITripRepositoryAsync : IGenericRepositoryAsync<Trip>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedTripResponseAsync(GetTripsQuery requestParameters);
    }
}
