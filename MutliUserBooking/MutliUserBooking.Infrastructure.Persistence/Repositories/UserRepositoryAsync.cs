using LinqKit;
using Microsoft.EntityFrameworkCore;
using MutliUserBooking.Application.Features.User.Queries.GetUsers;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Domain.Entities;
using MutliUserBooking.Infrastructure.Persistence.Contexts;
using MutliUserBooking.Infrastructure.Persistence.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MutliUserBooking.Infrastructure.Persistence.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly IDataShapeHelper<User> _dataShaper;
        private readonly DbSet<User> _users;



        /// <summary>
        /// Constructor for UserRepositoryAsync class.
        /// </summary>
        /// <param name="dbContext">ApplicationDbContext object.</param>
        /// <param name="dataShaper">IDataShapeHelper object.</param>
        /// <returns>
        /// 
        /// </returns>
        public UserRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<User> dataShaper) : base(dbContext)
        {
            _dataShaper = dataShaper;
            _users = dbContext.Set<User>();
        }



        /// <summary>
        /// Retrieves a paged list of Users based on the provided query parameters.
        /// </summary>
        /// <param name="requestParameters">The query parameters used to filter and page the data.</param>
        /// <returns>A tuple containing the paged list of Users and the total number of records.</returns>
        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedUserResponseAsync(GetUsersQuery requestParameters)
        {
            IQueryable<User> result;

            var email = requestParameters.Email;
            var password = requestParameters.Password;

            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            result = _users
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = result.Count();

            // filter data
            FilterByColumn(ref result, email, password);

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
                result = result.Select<User>("new(" + fields + ")");
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
        /// <param name="email">The email to filter by</param>
        /// <param name="password">The last password to filter by.</param>
        private void FilterByColumn(ref IQueryable<User> Users, string email, string password)
        {
            if (!Users.Any())
                return;

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
                return;

            var predicate = PredicateBuilder.New<User>();

            if (!string.IsNullOrEmpty(email))
                predicate = predicate.Or(p => p.Email.ToLower().Contains(email.ToLower().Trim()));

            if (!string.IsNullOrEmpty(password))
                predicate = predicate.Or(p => p.Password.ToLower().Contains(password.ToLower().Trim()));


            Users = Users.Where(predicate);
        }
    }
}