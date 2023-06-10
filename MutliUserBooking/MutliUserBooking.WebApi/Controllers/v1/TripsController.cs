using Microsoft.AspNetCore.Mvc;
using MutliUserBooking.Application.Features.Trips.Queries.GetTrips;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MutliUserBooking.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TripsController : BaseApiController
    {

        /// <summary>
        /// Gets a list of Trips based on the specified filter.
        /// </summary>
        /// <param name="filter">The filter used to get the list of Trips.</param>
        /// <returns>A list of Trips.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTripsQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

    }
}