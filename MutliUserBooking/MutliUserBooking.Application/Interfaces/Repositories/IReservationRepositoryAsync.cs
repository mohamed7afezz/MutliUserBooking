using MutliUserBooking.Application.Features.Reservations.Queries.GetReservations;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged Reservation response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface IReservationRepositoryAsync : IGenericRepositoryAsync<Reservation>
    {
        Task<bool> IsValidUserIdAsync(Guid UserId);
        Task<bool> IsValidTripIdAsync(Guid TripId);
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedReservationResponseAsync(GetReservationsQuery requestParameters);
    }
}