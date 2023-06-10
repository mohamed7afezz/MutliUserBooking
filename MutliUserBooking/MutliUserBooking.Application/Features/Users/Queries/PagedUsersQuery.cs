using AutoMapper;
using MediatR;
using MutliUserBooking.Application.Features.User.Queries.GetUsers;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Application.Wrappers;
using MutliUserBooking.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.Users.Queries.GetUsers
{
    public partial class PagedUsersQuery : IRequest<PagedDataTableResponse<IEnumerable<Entity>>>
    {
        //strong type input parameters
        public int Draw { get; set; } //page number
        public int Start { get; set; } //Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        public int Length { get; set; } //page size
        public IList<Order> Order { get; set; } //Order by
        public Search Search { get; set; } //search criteria
        public IList<Column> Columns { get; set; } //select fields
    }

    public class PageUserQueryHandler : IRequestHandler<PagedUsersQuery, PagedDataTableResponse<IEnumerable<Entity>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for PageUserQueryHandler class.
        /// </summary>
        /// <param name="userRepository">IUserRepositoryAsync object.</param>
        /// <param name="mapper">IMapper object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// PageUserQueryHandler object.
        /// </returns>
        public PageUserQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the PagedUsersQuery request and returns a PagedDataTableResponse.
        /// </summary>
        /// <param name="request">The PagedUsersQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedDataTableResponse.</returns>
        public async Task<PagedDataTableResponse<IEnumerable<Entity>>> Handle(PagedUsersQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetUsersQuery();

            // Draw map to PageNumber
            validFilter.PageNumber = (request.Start / request.Length) + 1;
            // Length map to PageSize
            validFilter.PageSize = request.Length;

            // Map order > OrderBy
            var colOrder = request.Order[0];
            switch (colOrder.Column)
            {
                case 0:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "LastName" : "LastName DESC";
                    break;

                case 1:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "FirstName" : "FirstName DESC";
                    break;

                case 2:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "UserTitle" : "UserTitle DESC";
                    break;
                case 3:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "Email" : "Email DESC";
                    break;
            }

            // Map Search > searchable columns
            if (!string.IsNullOrEmpty(request.Search.Value))
            {
                //limit to fields in view model
                validFilter.Email = request.Search.Value;
                validFilter.Password = request.Search.Value;
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetUsersViewModel>();
            }
            // query based on filter
            var entityUsers = await _userRepository.GetPagedUserResponseAsync(validFilter);
            var data = entityUsers.data;
            RecordsCount recordCount = entityUsers.recordsCount;

            // response wrapper
            return new PagedDataTableResponse<IEnumerable<Entity>>(data, request.Draw, recordCount);
        }
    }
}