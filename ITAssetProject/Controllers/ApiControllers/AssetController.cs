using ITAssets.Models;
using ITAssets.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITAssets.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IITAssetRepository _assetRepository;

        public AssetController(IITAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        // GET: api/<AssetController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ITAssetList = await _assetRepository.GetAllAsync();
            return Ok(ITAssetList);
        }

        // GET api/<AssetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ITAsset = await _assetRepository.Get(id);

            return Ok(ITAsset);
        }

        // POST api/<AssetController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ITAsset iTAsset)
        {
            if (ModelState.IsValid)
            {
                if (await _assetRepository.IsSerialNumberExist(iTAsset.SerialNumber))
                {
                    ModelState.AddModelError(nameof(iTAsset.SerialNumber), "This serial number already used");
                    return BadRequest(ModelState);
                }

                await _assetRepository.Add(iTAsset);
                await _assetRepository.SaveChanges();

                return Ok(iTAsset);
            }

            return BadRequest(ModelState);
        }

        // PUT api/<AssetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ITAsset iTAsset)
        {
            if (ModelState.IsValid)
            {
                _assetRepository.Update(id, iTAsset);
                await _assetRepository.SaveChanges();
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE api/<AssetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _assetRepository.Delete(id);
            await _assetRepository.SaveChanges();

            return NoContent();
        }
    }
}
