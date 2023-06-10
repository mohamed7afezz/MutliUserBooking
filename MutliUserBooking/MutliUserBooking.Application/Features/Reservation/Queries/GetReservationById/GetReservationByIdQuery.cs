using MediatR;
using MutliUserBooking.Application.Exceptions;
using MutliUserBooking.Application.Features.Reservations.Queries.GetReservations;
using MutliUserBooking.Application.Helpers;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Parameters;
using MutliUserBooking.Application.Wrappers;
using MutliUserBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdQuery : IRequest<Response<Entity>>
    {
        public Guid Id { get; set; }

        public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Response<Entity>>
        {
            private readonly IReservationRepositoryAsync _reservationRepository;
            private readonly IModelHelper _modelHelper;

            public GetReservationByIdQueryHandler(IReservationRepositoryAsync reservationRepository, IModelHelper modelHelper)
            {
                _reservationRepository = reservationRepository;
                _modelHelper = modelHelper;
            }

            public async Task<Response<Entity>> Handle(GetReservationByIdQuery query, CancellationToken cancellationToken)
            {
                //TODO:: make one generic 
                //var reservation = await _reservationRepository.GetByIdAsync(query.Id);

                var validFilter = new GetReservationsQuery();

                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetReservationsViewModel>();
                validFilter.Id = query.Id;

                // query based on filter
                var entityReservations = await _reservationRepository.GetPagedReservationResponseAsync(validFilter);
                var data = entityReservations.data.FirstOrDefault();
               
                if (data == null) throw new ApiException($"Reservation Not Found.");
                return new Response<Entity>(data);
            }
        }
    }
}