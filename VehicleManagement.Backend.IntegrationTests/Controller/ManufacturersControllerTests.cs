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
    public class ManufacturersControllerTests : IClassFixture<VehicleManagementTestServer>
    {
        private const string _baseUrl = "api/Manufacturers";

        private readonly Mock<IManufacturerDomain> _manufacturerDomainMock;
        private readonly HttpClient _httpClient;

        public ManufacturersControllerTests(VehicleManagementTestServer factory)
        {
            _manufacturerDomainMock = new Mock<IManufacturerDomain>();
            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(provider => _manufacturerDomainMock.Object);
                });
            })
            .CreateClient();
        }

        [Theory]
        [MemberData(nameof(ManufacturerTestData.GetManufacturersTestData), MemberType = typeof(ManufacturerTestData))]
        public async Task GetAll_Should_Return_Ok_Result(List<Manufacturer> manufacturers)
        {
            // ARRANGE
            _manufacturerDomainMock
                .Setup(vd => vd.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(manufacturers);

            // ACT
            var response = await _httpClient.GetAsync(_baseUrl);

            // ASSERT
            HttpAssertions.AssertSuccess(response);

            var body = await response.GetBodyAs<IEnumerable<Manufacturer>>();

            Assert.NotNull(body);
            body.Should().BeEquivalentTo(manufacturers);
            Assert.Equal(manufacturers.Count, body.Count());
        }

        [Fact]
        public async Task GetAll_With_DataConversionException_Should_Return_BadRequest()
        {
            // ARRANGE
            _manufacturerDomainMock
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
