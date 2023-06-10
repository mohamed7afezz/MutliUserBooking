using FluentValidation;
using MutliUserBooking.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Features.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
    {
        private readonly IReservationRepositoryAsync reservationRepository;

        public UpdateReservationCommandValidator(IReservationRepositoryAsync reservationRepository)
        {
            this.reservationRepository = reservationRepository;

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(IsValidUser).WithMessage("{PropertyName} is not exists.");

            RuleFor(p => p.CustomerName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.TripId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(IsValidTrip).WithMessage("{PropertyName} is not exists.");
        }

        private async Task<bool> IsValidUser(Guid UserId, CancellationToken cancellationToken)
        {
            return await reservationRepository.IsValidUserIdAsync(UserId);
        }
        private async Task<bool> IsValidTrip(Guid TripId, CancellationToken cancellationToken)
        {
            return await reservationRepository.IsValidTripIdAsync(TripId);
        }
    }
}