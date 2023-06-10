using AutoMapper;
using MediatR;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Application.Wrappers;
using MutliUserBooking.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.Reservations.Commands.CreateReservation
{
    public partial class CreateReservationCommand : IRequest<Response<Guid>>
    {
        public Guid UserId { get; set; }
        public string CustomerName { get; set; }
        public Guid TripId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Note { get; set; }
    }

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Response<Guid>>
    {
        private readonly IReservationRepositoryAsync _reservationRepository;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly ITripRepositoryAsync _tripRepository;
        private readonly IMapper _mapper;

        public CreateReservationCommandHandler(IReservationRepositoryAsync reservationRepository,
            IUserRepositoryAsync userRepository,
            ITripRepositoryAsync tripRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _tripRepository = tripRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = _mapper.Map<Reservation>(request);
            
            // Define items that will be done server side
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var trip = await _tripRepository.GetByIdAsync(request.TripId);
            reservation.CreationDate = DateTime.UtcNow;

            reservation.ReservedBy = user;
            reservation.Trip = trip;
            
            await _reservationRepository.AddReservationAsync(reservation);
            return new Response<Guid>(reservation.Id);
        }
    }
}