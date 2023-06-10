using Microsoft.AspNetCore.Mvc;
using MutliUserBooking.Application.Features.User.Queries.GetUsers;
using MutliUserBooking.Application.Features.Users.Queries.GetUsers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MutliUserBooking.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UsersController : BaseApiController
    {

        /// <summary>
        /// Gets a list of employees based on the specified filter.
        /// </summary>
        /// <param name="filter">The filter used to get the list of employees.</param>
        /// <returns>A list of employees.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

    }
}