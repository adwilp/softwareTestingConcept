using System.Linq;
using System.Threading;

using Moq;

using VehicleManagement.DBAccess.IntegrationTests.DBFixtures;
using VehicleManagement.DBAccess.Repositories;

namespace VehicleManagement.DBAccess.IntegrationTests.Repositories
{
    public class VehicleRepositoryTests
    {
        private readonly IVehicleRepository _repository;

        public VehicleRepositoryTests()
        {
            var dbFixture = new VehicleDBFixture();
            var context = dbFixture.CreateContext();

            _repository = new VehicleRepository(context);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All()
        {
            // ACT
            var result = await _repository.GetAllAsync(It.IsAny<CancellationToken>());
            var resultList = result.ToList();

            // ASSERT
            Assert.Equal(3, resultList.Count);

            Assert.Equal("SB164ABN1PE082986", resultList[0].FIN);
            Assert.Equal("MI-XY-666", resultList[0].LicensePlate);
            Assert.Equal("black", resultList[0].Color);
            Assert.Equal(12345.89, resultList[0].Mileage);
            Assert.Equal("WMI", resultList[0].ManufacturerId);

            Assert.Equal("SB164ABN1PE082096", resultList[1].FIN);
            Assert.Equal("DH-IL-12", resultList[1].LicensePlate);
            Assert.Equal("green", resultList[1].Color);
            Assert.Equal(125.40, resultList[1].Mileage);
            Assert.Equal("W0L", resultList[1].ManufacturerId);

            Assert.Equal("SB189ABN1PE034986", resultList[2].FIN);
            Assert.Equal("VEC-KL-234", resultList[2].LicensePlate);
            Assert.Equal("red", resultList[2].Color);
            Assert.Equal(0.0, resultList[2].Mileage);
            Assert.Equal("WMI", resultList[2].ManufacturerId);
        }
    }
}
