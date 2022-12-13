using System.Linq;
using System.Threading;

using Moq;

using VehicleManagement.DBAccess.IntegrationTests.DBFixtures;
using VehicleManagement.DBAccess.Repositories;

namespace VehicleManagement.DBAccess.IntegrationTests.Repositories
{
    public class ManufacturerRepositoryTests
    {
        private readonly IManufacturerRepository _repository;

        public ManufacturerRepositoryTests()
        {
            var dbFixture = new ManufacturerDBFixture();
            var context = dbFixture.CreateContext();

            _repository = new ManufacturerRepository(context);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All()
        {
            // ACT
            var result = await _repository.GetAllAsync(It.IsAny<CancellationToken>());
            var resultList = result.ToList();

            // ASSERT
            Assert.Equal(2, resultList.Count);

            Assert.Equal("WMI", resultList[0].Id);
            Assert.Equal("Audi", resultList[0].Name);

            Assert.Equal("W0L", resultList[1].Id);
            Assert.Equal("Opel", resultList[1].Name);
        }
    }
}
