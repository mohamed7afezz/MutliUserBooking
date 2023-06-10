using AutoMapper;
using MutliUserBooking.Application.Features.Employees.Queries.GetEmployees;
using MutliUserBooking.Application.Features.Positions.Commands.CreatePosition;
using MutliUserBooking.Application.Features.Positions.Queries.GetPositions;
using MutliUserBooking.Application.Features.Reservations.Commands.CreateReservation;
using MutliUserBooking.Application.Features.Reservations.Queries.GetReservations;
using MutliUserBooking.Application.Features.Trips.Queries.GetTrips;
using MutliUserBooking.Application.Features.User.Queries.GetUsers;
using MutliUserBooking.Domain.Entities;

namespace MutliUserBooking.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, GetUsersViewModel>().ReverseMap();
            CreateMap<Trip, GetTripsViewModel>().ReverseMap();
            CreateMap<User, GetUsersViewModel>().ReverseMap();
            CreateMap<Reservation, GetReservationsViewModel>().ReverseMap();
            CreateMap<CreateReservationCommand, Reservation>();

            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();
        }
    }
}