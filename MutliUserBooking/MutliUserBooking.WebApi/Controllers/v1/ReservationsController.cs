using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MutliUserBooking.Application.Features.Reservations.Commands.CreateReservation;
using MutliUserBooking.Application.Features.Reservations.Commands.DeleteReservationById;
using MutliUserBooking.Application.Features.Reservations.Commands.UpdateReservation;
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

        /// <summary>
        /// Creates a new Reservation.
        /// </summary>
        /// <param name="command">The command containing the data for the new Reservation.</param>
        /// <returns>A 201 Created response containing the newly created Reservation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateReservationCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        /// <summary>
        /// Updates a Reservation with the given id using the provided command.
        /// </summary>
        /// <param name="id">The id of the Reservation to update.</param>
        /// <param name="command">The command containing the updated information.</param>
        /// <returns>The updated Reservation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateReservationCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a Reservation by its Id.
        /// </summary>
        /// <param name="id">The Id of the Reservation to delete.</param>
        /// <returns>The result of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteReservationByIdCommand { Id = id }));
        }
    }
}