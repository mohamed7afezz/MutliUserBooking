using MutliUserBooking.Domain.Enums;
using System;

namespace MutliUserBooking.Application.Features.User.Queries.GetUsers
{
    public class GetUsersViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}