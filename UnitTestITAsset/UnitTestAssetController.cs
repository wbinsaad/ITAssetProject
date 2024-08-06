using ITAssetProject.Models;
using ITAssets.Repository;
using ITAssetsProject.Controllers.ApiControllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTestITAsset
{
    public class UnitTestAssetController
    {
        [Fact]
        public async Task TestGetAllAsync()
        {
            var mockITAssetRepository = new Mock<ITAssetRepository>();

            mockITAssetRepository.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<ITAsset>());
            var assetController = new AssetController(mockITAssetRepository.Object);

            OkObjectResult result = (OkObjectResult)await assetController.Get();

            Assert.Equal(result.StatusCode, 200);
        }

        [Fact]
        public async Task TestGet()
        {
            var mockITAssetRepository = new Mock<ITAssetRepository>();

            mockITAssetRepository.Setup(service => service.Get(Guid.Empty)).ReturnsAsync(new ITAsset());
            var assetController = new AssetController(mockITAssetRepository.Object);

            var result = (OkObjectResult)await assetController.Get(Guid.Empty);

            Assert.Equal(result.StatusCode, 200);
        }

        [Fact]
        public async Task TestAdd()
        {
            var mockITAssetRepository = new Mock<ITAssetRepository>();

            mockITAssetRepository.Setup(service => service.Add(new ITAsset())).ReturnsAsync(new ITAsset());
            mockITAssetRepository.Setup(service => service.IsSerialNumberExist(string.Empty)).ReturnsAsync(false);
            mockITAssetRepository.Setup(service => service.SaveChanges()).Returns(Task.CompletedTask);

            var assetController = new AssetController(mockITAssetRepository.Object);

            var result = (OkObjectResult)await assetController.Post(new ITAsset());

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task TestDelete()
        {
            var mockITAssetRepository = new Mock<ITAssetRepository>();

            mockITAssetRepository.Setup(service => service.Delete(Guid.Empty));
            mockITAssetRepository.Setup(service => service.SaveChanges()).Returns(Task.CompletedTask);

            var assetController = new AssetController(mockITAssetRepository.Object);

            var result = (NoContentResult)await assetController.Delete(Guid.Empty);

            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task TestEdit()
        {
            var mockITAssetRepository = new Mock<ITAssetRepository>();

            mockITAssetRepository.Setup(service => service.Update(Guid.Empty, new ITAsset()));
            mockITAssetRepository.Setup(service => service.SaveChanges()).Returns(Task.CompletedTask);

            var assetController = new AssetController(mockITAssetRepository.Object);

            var result = (NoContentResult)await assetController.Put(Guid.Empty, new ITAsset());

            Assert.Equal(204, result.StatusCode);
        }
    }
}
