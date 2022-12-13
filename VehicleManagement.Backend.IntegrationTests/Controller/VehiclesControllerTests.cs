using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using VehicleManagement.Backend.Exceptions;
using VehicleManagement.Backend.IntegrationTests.Extensions;
using VehicleManagement.Backend.IntegrationTests.TestData;
using VehicleManagement.Backend.IntegrationTests.Utilities;
using VehicleManagement.Core.Domains;
using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;

namespace VehicleManagement.Backend.IntegrationTests.Controller
{
    public class VehiclesControllerTests : IClassFixture<VehicleManagementTestServer>
    {
        private const string _baseUrl = "api/Vehicles";

        private readonly Mock<IVehicleDomain> _vehicleDomainMock;
        private readonly HttpClient _httpClient;

        public VehiclesControllerTests(VehicleManagementTestServer factory)
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
        public async Task GetAll_Should_Return_Ok_Result(List<FlatVehicle> vehicles)
        {
            // ARRANGE
            _vehicleDomainMock
                .Setup(vd => vd.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicles);

            // ACT
            var response = await _httpClient.GetAsync(_baseUrl);

            // ASSERT
            HttpAssertions.AssertSuccess(response);

            var body = await response.GetBodyAs<IEnumerable<FlatVehicle>>();

            Assert.NotNull(body);
            body.Should().BeEquivalentTo(vehicles);
            Assert.Equal(vehicles.Count, body.Count());
        }

        [Fact]
        public async Task GetAll_With_DataConversionException_Should_Return_BadRequest()
        {
            // ARRANGE
            _vehicleDomainMock
                .Setup(vd => vd.GetAllAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DataConversionException(It.IsAny<string>(), It.IsAny<string>()));

            // ACT
            var response = await _httpClient.GetAsync(_baseUrl);

            // ASSERT
            HttpAssertions.AssertBadRequest(response);

            var body = await response.GetBodyAs<ErrorResponse>();

            Assert.NotNull(body);
        }
    }
}
