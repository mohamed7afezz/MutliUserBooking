using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MutliUserBooking.Application.Features.Trips.Queries.GetTrips;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Domain.Entities;
using MutliUserBooking.Infrastructure.Persistence.Contexts;
using MutliUserBooking.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MutliUserBooking.Infrastructure.Persistence.Repositories
{
    public class TripRepositoryAsync : GenericRepositoryAsync<Trip>, ITripRepositoryAsync
    {
        private readonly IDataShapeHelper<Trip> _dataShaper;
        private readonly DbSet<Trip> _trips;



        /// <summary>
        /// Constructor for TripRepositoryAsync class.
        /// </summary>
        /// <param name="dbContext">ApplicationDbContext object.</param>
        /// <param name="dataShaper">IDataShapeHelper object.</param>
        /// <returns>
        /// 
        /// </returns>
        public TripRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Trip> dataShaper) : base(dbContext)
        {
            _dataShaper = dataShaper;
            _trips = dbContext.Set<Trip>();
        }



        /// <summary>
        /// Retrieves a paged list of Trips based on the provided query parameters.
        /// </summary>
        /// <param name="requestParameters">The query parameters used to filter and page the data.</param>
        /// <returns>A tuple containing the paged list of Trips and the total number of records.</returns>
        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedTripResponseAsync(GetTripsQuery requestParameters)
        {
            IQueryable<Trip> result;

            var name = requestParameters.Name;
            var cityName = requestParameters.CityName;
            var price = requestParameters.Price;
            var creationDate = requestParameters.CreationDate;

            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            result = _trips
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = result.Count();

            // filter data
            FilterByColumn(ref result, name, cityName, price, creationDate);

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
                result = result.Select<Trip>("new(" + fields + ")");
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
        /// Filters an IQueryable of Users based on the provided parameters.
        /// </summary>
        /// <param name="Users">The IQueryable of Users to filter.</param>
        /// <param name="name">The name to filter by</param>
        /// <param name="cityName">The last cityName to filter by.</param>
        /// <param name="price">The last price to filter by.</param>
        /// <param name="cityName">The last cityName to filter by.</param>
        private void FilterByColumn(ref IQueryable<Trip> Trips, string name, string cityName, decimal? price, DateTime? creationDate)
        {
            if (!Trips.Any())
                return;

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cityName) && decimal.Equals(price, null) && DateTime.Equals(creationDate, null))
                return;

            var predicate = PredicateBuilder.New<Trip>();

            if (!string.IsNullOrEmpty(name))
                predicate = predicate.Or(p => p.Name.ToLower().Contains(name.ToLower().Trim()));

            if (!string.IsNullOrEmpty(cityName))
                predicate = predicate.Or(p => p.CityName.ToLower().Contains(cityName.ToLower().Trim()));

            if (!decimal.Equals(price, null))
                predicate = predicate.Or(p => p.Price.Equals(price));

            if (!DateTime.Equals(creationDate, null))
                predicate = predicate.Or(p => p.CreationDate.Equals(creationDate));

            Trips = Trips.Where(predicate);
        }
    }
}