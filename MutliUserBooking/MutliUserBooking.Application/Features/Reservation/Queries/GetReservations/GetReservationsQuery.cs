using AutoMapper;
using MediatR;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Application.Wrappers;
using MutliUserBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.Reservations.Queries.GetReservations
{
    public class GetReservationsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public Guid? UserId { get; set; }
        public string CustomerName { get; set; }
        public Guid? TripId { get; set; }
    }

    public class GetAllReservationsQueryHandler : IRequestHandler<GetReservationsQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IReservationRepositoryAsync _reservationRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllReservationsQueryHandler(IReservationRepositoryAsync reservationRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {

            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetReservationsViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetReservationsViewModel>();
            }
            // query based on filter
            var entityReservations = await _reservationRepository.GetPagedReservationResponseAsync(validFilter);
            var data = entityReservations.data;
            RecordsCount recordCount = entityReservations.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}