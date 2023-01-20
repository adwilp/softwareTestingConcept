using System.Net;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using VehicleManagement.Backend.Exceptions;
using VehicleManagement.Core.Domains;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Backend.Controllers
{
    /// <summary>
    /// Controller for all enpoints regarding Vehicle entity.
    /// </summary>
    /// <seealso cref="VehicleManagement.Backend.Controllers.BaseController" />
    public class VehiclesController : BaseController
    {
        private readonly IVehicleDomain _vehicleDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="vehicleDomain">The vehicle domain.</param>
        public VehiclesController(IVehicleDomain vehicleDomain)
        {
            _vehicleDomain = vehicleDomain;
        }

        /// <summary>
        /// Gets all vehicles.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of all available vehicles.</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FlatVehicle>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _vehicleDomain.GetAllAsync(cancellationToken));
        }
    }
}
