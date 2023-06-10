using LinqKit;
using Microsoft.EntityFrameworkCore;
using MutliUserBooking.Application.Features.Reservations.Queries.GetReservations;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Domain.Entities;
using MutliUserBooking.Infrastructure.Persistence.Contexts;
using MutliUserBooking.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MutliUserBooking.Infrastructure.Persistence.Repositories
{
    public class ReservationRepositoryAsync : GenericRepositoryAsync<Reservation>, IReservationRepositoryAsync
    {
        private readonly IDataShapeHelper<Reservation> _dataShaper;
        private readonly DbSet<Reservation> _reservations;



        /// <summary>
        /// Constructor for ReservationRepositoryAsync class.
        /// </summary>
        /// <param name="dbContext">ApplicationDbContext object.</param>
        /// <param name="dataShaper">IDataShapeHelper object.</param>
        /// <returns>
        /// 
        /// </returns>
        public ReservationRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Reservation> dataShaper) : base(dbContext)
        {
            _dataShaper = dataShaper;
            _reservations = dbContext.Set<Reservation>();
        }



        /// <summary>
        /// Retrieves a paged list of Reservations based on the provided query parameters.
        /// </summary>
        /// <param name="requestParameters">The query parameters used to filter and page the data.</param>
        /// <returns>A tuple containing the paged list of Reservations and the total number of records.</returns>
        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedReservationResponseAsync(GetReservationsQuery requestParameters)
        {
            IQueryable<Reservation> result;

            var reservedBy = requestParameters.UserId;
            var customerName = requestParameters.CustomerName;
            var tripId = requestParameters.TripId;

            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            result = _reservations
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = result.Count();

            // filter data
            FilterByColumn(ref result, reservedBy, customerName, tripId);

            // Count records after filter
            recordsFiltered = result.Count();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            //limit query fields
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Reservation>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            // retrieve data to list
            // var resultData = await result.ToListAsync();
            // Note: Bogus library does not support await for AsQueryable.
            // Workaround:  fake await with Task.Run and use regular ToList
            var resultData = await Task.Run(() => result.ToList());

            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }



        /// <summary>
        /// Filters an IQueryable of Reservations based on the provided parameters.
        /// </summary>
        /// <param name="Reservations">The IQueryable of Reservations to filter.</param>
        /// <param name="reservedBy">The reservedBy to filter by</param>
        /// <param name="customerName">The last customerName to filter by.</param>
        /// <param name="tripId">The last tripId to filter by.</param>
        private void FilterByColumn(ref IQueryable<Reservation> Reservations, Guid? reservedBy, string customerName, Guid? tripId)
        {
            if (!Reservations.Any())
                return;

            if (Guid.Equals(reservedBy, null) && string.IsNullOrEmpty(customerName) && Guid.Equals(tripId, null))
                return;

            var predicate = PredicateBuilder.New<Reservation>();

            if (!Guid.Equals(reservedBy, null))
                predicate = predicate.Or(p => p.ReservedBy.Id.Equals(reservedBy));

            if (!string.IsNullOrEmpty(customerName))
                predicate = predicate.Or(p => p.CustomerName.ToLower().Contains(customerName.ToLower().Trim()));

            if (!Guid.Equals(tripId, null))
                predicate = predicate.Or(p => p.Trip.Id.Equals(tripId));


            Reservations = Reservations.Where(predicate);
        }
    }
}