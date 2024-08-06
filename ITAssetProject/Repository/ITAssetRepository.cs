using ITAssetProject.Database;
using ITAssetProject.Models;
using ITAssetProject.Repository;
using ITAssetProject.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace ITAssets.Repository
{
    public class ITAssetRepository : IITAssetRepository, IDisposable
    {
        private readonly AppDbContext _DbContext;

        public ITAssetRepository()
        {

        }

        public ITAssetRepository(AppDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        /// <summary>
        ///     Add a new Asset
        /// </summary>
        public virtual async Task<ITAsset> Add(ITAsset asset)
        {
            await _DbContext.AddAsync(asset);
            return asset;
        }

        /// <summary>
        ///     Delete a Asset by ID Guid
        /// </summary>
        public virtual void Delete(Guid id)
        {
            var ITAssetQuery = _DbContext
                                            .ITAsset
                                            .Where(asset => asset.Id == id)
                                            .FirstOrDefault();

            if (ITAssetQuery is null) return;

            _DbContext.Remove(ITAssetQuery);

            return;
        }

        /// <summary>
        ///     Get a Asset by ID Guid
        /// </summary>
        public virtual async Task<ITAsset?> Get(Guid id)
        {
            var ITAssetQuery = await _DbContext
                                            .ITAsset
                                            .Where(asset => asset.Id == id)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();

            return ITAssetQuery;
        }

        /// <summary>
        ///     Get List of all Asset
        /// </summary>
        public virtual async Task<List<ITAsset>> GetAllAsync()
        {
            var ITAssetQuery = _DbContext.ITAsset.AsQueryable();

            // Search

            // Order by the latest asset were entered
            ITAssetQuery = ITAssetQuery.OrderByDescending(asset => asset.CreatedDate);

            // Limit the number of asset for each request
            ITAssetQuery = ITAssetQuery.Skip(count: 0).Take(100);

            return await ITAssetQuery.AsNoTracking().ToListAsync();
        }

        /// <summary>
        ///     Update Asset
        /// </summary>
        public virtual void Update(Guid id, ITAsset asset)
        {
            _DbContext.Update(asset);
        }

        /// <summary>
        ///     Save Changes After done
        /// </summary>
        public virtual async Task SaveChanges()
        {
            await _DbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Get list of Assets By Brand for Dashboard
        /// </summary>
        public virtual async Task<List<AssetByBrand>> GetAssetsByBrand()
        {
            var AssetsQuery = await _DbContext
                                                  .ITAsset
                                                  .GroupBy(asset => asset.Brand)
                                                  .Select(item => new AssetByBrand
                                                  {
                                                      BrandName = item.Key,
                                                      NumberOfAssets = item.Count()
                                                  })
                                                  .OrderByDescending(item => item.NumberOfAssets)
                                                  .Skip(0)
                                                  .Take(100)
                                                  .AsNoTracking()
                                                  .ToListAsync();

            return AssetsQuery;
        }

        /// <summary>
        ///     Get list of Assets By Status for Dashboard
        /// </summary>
        public virtual async Task<List<AssetByStatus>> GetAssetsByStatus()
        {
            var AssetStatusList = await _DbContext.ITAsset
                .GroupBy(asset => asset.Status)
                .Select(asset => new AssetByStatus
                {
                    Status = asset.Key,
                    NumberOfAssets = asset.Count()
                })
                .OrderByDescending(item => item.NumberOfAssets)
                .AsNoTracking()
                .ToListAsync();

            return AssetStatusList;
        }

        /// <summary>
        ///     To check the SerialNumber if is already exist to avoid duplication
        /// </summary>
        public virtual async Task<bool> IsSerialNumberExist(string SerialNumber)
        {
            var query = await _DbContext
                                 .ITAsset
                                 .AnyAsync(asset => asset.SerialNumber == SerialNumber);

            return query;
        }

        public void Dispose()
        {
            _DbContext.Dispose();
        }
    }
}
