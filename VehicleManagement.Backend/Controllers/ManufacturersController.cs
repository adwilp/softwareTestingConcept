using System.Net;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using VehicleManagement.Backend.Exceptions;
using VehicleManagement.Core.Domains;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Backend.Controllers
{
    /// <summary>
    /// Controller for all enpoints regarding Manufacturer entity.
    /// </summary>
    /// <seealso cref="VehicleManagement.Backend.Controllers.BaseController" />
    public class ManufacturersController : BaseController
    {
        private readonly IManufacturerDomain _manufacturerDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturersController"/> class.
        /// </summary>
        /// <param name="manufacturerDomain">The manufacturer domain.</param>
        public ManufacturersController(IManufacturerDomain manufacturerDomain)
        {
            _manufacturerDomain = manufacturerDomain;
        }

        /// <summary>
        /// Gets all manufacturers.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of all manufacturers.</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Manufacturer>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _manufacturerDomain.GetAllAsync(cancellationToken));
        }
    }
}
