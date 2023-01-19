using System.Net;

using Microsoft.AspNetCore.Mvc;

using VehicleManagement.Core.Domains;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Backend.Controllers
{
    public class BookingsController : BaseController
    {
        private readonly IBookingDomain _bookingDomain;

        public BookingsController(IBookingDomain bookingDomain)
        {
            _bookingDomain = bookingDomain;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _bookingDomain.GetAllAsync(cancellationToken));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await _bookingDomain.GetAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Booking booking, CancellationToken cancellationToken)
        {
            var newBooking = await _bookingDomain.AddAsync(booking, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created, newBooking);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateableBooking booking, CancellationToken cancellationToken)
        {
            var updatedBooking = await _bookingDomain.UpdateAsync(booking, cancellationToken);

            return Ok(updatedBooking);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _bookingDomain.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
