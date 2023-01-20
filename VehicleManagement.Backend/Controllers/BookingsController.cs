using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

using Swashbuckle.AspNetCore.Annotations;

using VehicleManagement.Backend.Exceptions;
using VehicleManagement.Core.Domains;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Backend.Controllers
{
    /// <summary>
    /// Controller for all enpoints regarding Booking entity.
    /// </summary>
    /// <seealso cref="VehicleManagement.Backend.Controllers.BaseController" />
    public class BookingsController : BaseController
    {
        private readonly IBookingDomain _bookingDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingsController"/> class.
        /// </summary>
        /// <param name="bookingDomain">The booking domain.</param>
        public BookingsController(IBookingDomain bookingDomain)
        {
            _bookingDomain = bookingDomain;
        }

        /// <summary>
        /// Gets all bookings.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of all available bookings.</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FlatBooking>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _bookingDomain.GetAllAsync(cancellationToken));
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The specified booking.</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UpdateDataOperation))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await _bookingDomain.GetAsync(id, cancellationToken));
        }

        /// <summary>
        /// Adds the specified booking.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new booking.</returns>
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(FlatBooking))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Booking booking, CancellationToken cancellationToken)
        {
            var newBooking = await _bookingDomain.AddAsync(booking, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created, newBooking);
        }

        /// <summary>
        /// Updates the specified booking.
        /// </summary>
        /// <param name="booking">The booking update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated booking.</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(FlatBooking))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateableBooking booking, CancellationToken cancellationToken)
        {
            var updatedBooking = await _bookingDomain.UpdateAsync(booking, cancellationToken);

            return Ok(updatedBooking);
        }

        /// <summary>
        /// Deletes the specified booking.
        /// </summary>
        /// <param name="id">The booking id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content.</returns>
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _bookingDomain.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
