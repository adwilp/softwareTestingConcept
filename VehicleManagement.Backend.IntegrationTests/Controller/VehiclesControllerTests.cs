using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using VehicleManagement.Backend.IntegrationTests.TestData;
using VehicleManagement.Core.Domains;
using VehicleManagement.DataContracts.DataModels;

using Xunit;

namespace VehicleManagement.Backend.IntegrationTests.Controller
{
    public class VehiclesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<IVehicleDomain> _vehicleDomainMock;
        private readonly HttpClient _httpClient;

        public VehiclesControllerTests(WebApplicationFactory<Program> factory)
        {
            _vehicleDomainMock = new Mock<IVehicleDomain>();
            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(provider => _vehicleDomainMock.Object);
                });
            })
            .CreateClient();
        }

        [Theory]
        [MemberData(nameof(VehicleTestData.GetVehiclesTestData), MemberType = typeof(VehicleTestData))]
        public async Task Get_Should_Return_Ok_Result(List<FlatVehicle> vehicles)
        {
            // ARRANGE
            _vehicleDomainMock
                .Setup(vd => vd.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicles);

            // ACT
            var response = await _httpClient.GetAsync("api/Manufacturers");

            // ASSERT
            response.EnsureSuccessStatusCode();

            //var response = Assert.IsType<OkObjectResult>(result);
            //var body = Assert.IsAssignableFrom<IEnumerable<FlatVehicle>>(response.Value);

            //Assert.NotNull(body);
            //body.Should().BeEquivalentTo(vehicles);
            //Assert.Equal(vehicles.Count, body.Count());
        }
    }
}
