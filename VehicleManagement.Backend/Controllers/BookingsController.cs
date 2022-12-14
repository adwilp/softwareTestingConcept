using Microsoft.AspNetCore.Mvc;

using VehicleManagement.Core.Domains;

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
    }
}
