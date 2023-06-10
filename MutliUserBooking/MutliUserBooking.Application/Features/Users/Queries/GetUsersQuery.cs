using AutoMapper;
using MediatR;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Application.Wrappers;
using MutliUserBooking.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.User.Queries.GetUsers
{
    /// <summary>
    /// GetAllUsersQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetUsersQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        //examples:
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for GetAllUsersQueryHandler class.
        /// </summary>
        /// <param name="userRepository">IUserRepositoryAsync object.</param>
        /// <param name="mapper">IMapper object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllUsersQueryHandler object.
        /// </returns>
        public GetAllUsersQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the GetUsersQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetUsersQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetUsersViewModel>(validFilter.Fields);
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
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}
