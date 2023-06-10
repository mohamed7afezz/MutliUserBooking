

using AutoMapper;
using MediatR;
using MutliUserBooking.Application.Features.User.Queries.GetUsers;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Application.Wrappers;
using MutliUserBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace MutliUserBooking.Application.Features.Trips.Queries.GetTrips
{
    /// <summary>
    /// GetAllTripsQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetTripsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string Name { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreationDate { get; set; }
    }
    public class GetAllTripsQueryHandler : IRequestHandler<GetTripsQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly ITripRepositoryAsync _tripRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for GetAllTripsQueryHandler class.
        /// </summary>
        /// <param name="TripRepository">ITripRepositoryAsync object.</param>
        /// <param name="mapper">IMapper object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllTripsQueryHandler object.
        /// </returns>
        public GetAllTripsQueryHandler(ITripRepositoryAsync tripRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _tripRepository = tripRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the GetTripsQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetTripsQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetTripsViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetTripsViewModel>();
            }
            // query based on filter
            var entityTrips = await _tripRepository.GetPagedTripResponseAsync(validFilter);
            var data = entityTrips.data;
            RecordsCount recordCount = entityTrips.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}
