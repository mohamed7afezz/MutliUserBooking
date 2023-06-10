using Microsoft.AspNetCore.Mvc;
using MutliUserBooking.Application.Features.Reservations.Queries.GetReservationById;
using MutliUserBooking.Application.Features.Reservations.Queries.GetReservations;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MutliUserBooking.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReservationController : BaseApiController
    {

        /// <summary>
        /// Gets a list of reservation based on the specified filter.
        /// </summary>
        /// <param name="filter">The filter used to get the list of reservation.</param>
        /// <returns>A list of reservation.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetReservationsQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// Gets a reservation by its Id.
        /// </summary>
        /// <param name="id">The Id of the reservation.</param>
        /// <returns>The reservation with the specified Id.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetReservationByIdQuery { Id = id }));
        }

    }
}