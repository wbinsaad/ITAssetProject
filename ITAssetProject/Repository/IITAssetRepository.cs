using ITAssetProject.Models;
using ITAssetProject.ViewModels.Dashboard;

namespace ITAssetProject.Repository
{
    public interface IITAssetRepository
    {
        Task<List<ITAsset>> GetAllAsync();
        Task<ITAsset?> Get(Guid id);
        Task<ITAsset> Add(ITAsset asset);
        void Update(Guid id, ITAsset asset);
        void Delete(Guid id);
        Task SaveChanges();
        Task<List<AssetByBrand>> GetAssetsByBrand();
        Task<List<AssetByStatus>> GetAssetsByStatus();
        Task<bool> IsSerialNumberExist(string SerialNumber);
    }
}
