using MediatR;
using MutliUserBooking.Application.Exceptions;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CustomerName { get; set; }
        public Guid TripId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Notes { get; set; }

        public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Response<Guid>>
        {
            private readonly IReservationRepositoryAsync _reservationRepository;
            private readonly IUserRepositoryAsync _userRepository;
            private readonly ITripRepositoryAsync _tripRepository;

            public UpdateReservationCommandHandler(IReservationRepositoryAsync reservationRepository, IUserRepositoryAsync userRepository,
            ITripRepositoryAsync tripRepository)
            {
                _reservationRepository = reservationRepository;
                _userRepository = userRepository;
                _tripRepository = tripRepository;
            }

            public async Task<Response<Guid>> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
            {
                var reservation = await _reservationRepository.GetByIdAsync(command.Id);

                if (reservation == null)
                {
                    throw new ApiException($"Reservation Not Found.");
                }
                else
                {
                    var user = await _userRepository.GetByIdAsync(command.UserId);
                    var trip = await _tripRepository.GetByIdAsync(command.TripId);

                    reservation.ReservedBy = user;
                    reservation.CustomerName = command.CustomerName;
                    reservation.Trip = trip;
                    reservation.ReservationDate = command.ReservationDate;
                    reservation.Notes = command.Notes;
                    await _reservationRepository.UpdateAsync(reservation);
                    return new Response<Guid>(reservation.Id);
                }
            }
        }
    }
}