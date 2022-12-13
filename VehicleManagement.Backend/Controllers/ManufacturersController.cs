using Microsoft.AspNetCore.Mvc;

using VehicleManagement.Core.Domains;

namespace VehicleManagement.Backend.Controllers
{
    public class ManufacturersController : BaseController
    {
        private readonly IManufacturerDomain _manufacturerDomain;

        public ManufacturersController(IManufacturerDomain manufacturerDomain)
        {
            _manufacturerDomain = manufacturerDomain;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _manufacturerDomain.GetAllAsync(cancellationToken));
        }
    }
}
