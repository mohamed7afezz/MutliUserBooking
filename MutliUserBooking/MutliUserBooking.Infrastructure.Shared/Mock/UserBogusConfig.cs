using AutoBogus;
using Bogus;
using MutliUserBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutliUserBooking.Infrastructure.Shared.Mock
{
    internal class UserBogusConfig : AutoFaker<User>
    {
        public UserBogusConfig()
        {
            Randomizer.Seed = new Random(8675309);
            RuleFor(p => p.Id, f => Guid.NewGuid());
            RuleFor(p => p.Email, (f, p) => f.Internet.Email());
            RuleFor(p => p.Password, (f, p) => f.Internet.Password());
        }
    }
}
