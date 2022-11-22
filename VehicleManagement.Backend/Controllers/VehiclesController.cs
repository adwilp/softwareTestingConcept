using Microsoft.AspNetCore.Mvc;

using VehicleManagement.Core.Domains;

namespace VehicleManagement.Backend.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleDomain _vehicleDomain;

        public VehiclesController(IVehicleDomain vehicleDomain)
        {
            _vehicleDomain = vehicleDomain;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _vehicleDomain.GetAllAsync(cancellationToken));
        }
    }
}
