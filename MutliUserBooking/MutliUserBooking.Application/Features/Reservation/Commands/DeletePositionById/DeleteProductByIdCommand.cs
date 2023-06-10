using MediatR;
using MutliUserBooking.Application.Exceptions;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.Reservations.Commands.DeleteReservationById
{
    public class DeleteReservationByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteReservationByIdCommandHandler : IRequestHandler<DeleteReservationByIdCommand, Response<Guid>>
        {
            private readonly IReservationRepositoryAsync _reservationRepository;

            public DeleteReservationByIdCommandHandler(IReservationRepositoryAsync reservationRepository)
            {
                _reservationRepository = reservationRepository;
            }

            public async Task<Response<Guid>> Handle(DeleteReservationByIdCommand command, CancellationToken cancellationToken)
            {
                var reservation = await _reservationRepository.GetByIdAsync(command.Id);
                if (reservation == null) throw new ApiException($"Reservation Not Found.");
                await _reservationRepository.DeleteAsync(reservation);
                return new Response<Guid>(reservation.Id);
            }
        }
    }
}