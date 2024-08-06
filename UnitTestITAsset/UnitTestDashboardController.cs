using ITAssetProject.ViewModels.Dashboard;
using ITAssets.Repository;
using ITAssetsProject.Controllers.ApiControllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestITAsset
{
    public class UnitTestDashboardController
    {
        [Fact]
        public async Task TestGetAssetsByBrand()
        {
            var mockITAssetRepository = new Mock<ITAssetRepository>();

            mockITAssetRepository.Setup(service => service.GetAssetsByBrand()).ReturnsAsync(new List<AssetByBrand>());
            var assetController = new DashboardController(mockITAssetRepository.Object);

            OkObjectResult result = (OkObjectResult)await assetController.GetAssetsByBrand();

            Assert.Equal(result.StatusCode, 200);
        }


        [Fact]
        public async Task TestGetAssetsByStatus()
        {
            var mockITAssetRepository = new Mock<ITAssetRepository>();

            mockITAssetRepository.Setup(service => service.GetAssetsByStatus()).ReturnsAsync(new List<AssetByStatus>());
            var assetController = new DashboardController(mockITAssetRepository.Object);

            OkObjectResult result = (OkObjectResult)await assetController.GetAssetsByStatus();

            Assert.Equal(result.StatusCode, 200);
        }
    }
}
