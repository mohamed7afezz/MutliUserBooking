﻿using AutoMapper;
using MutliUserBooking.Application.Features.Employees.Queries.GetEmployees;
using MutliUserBooking.Application.Features.Positions.Commands.CreatePosition;
using MutliUserBooking.Application.Features.Positions.Queries.GetPositions;
using MutliUserBooking.Application.Features.User.Queries.GetUsers;
using MutliUserBooking.Domain.Entities;

namespace MutliUserBooking.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<User, GetUsersViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();
        }
    }
}