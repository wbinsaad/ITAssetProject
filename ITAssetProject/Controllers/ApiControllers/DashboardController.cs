using ITAssetProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITAssetsProject.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IITAssetRepository _assetRepository;

        public DashboardController(IITAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        [HttpGet("AssetsByBrand")]
        public async Task<IActionResult> GetAssetsByBrand()
        {
            var ITAssetList = await _assetRepository.GetAssetsByBrand();
            return Ok(ITAssetList);
        }

        [HttpGet("AssetsByStatus")]
        public async Task<IActionResult> GetAssetsByStatus()
        {
            var ITAssetList = await _assetRepository.GetAssetsByStatus();
            return Ok(ITAssetList);
        }
    }
}
