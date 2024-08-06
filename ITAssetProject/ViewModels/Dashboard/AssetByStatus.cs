using ITAssetProject.Models;
using System.Text.Json.Serialization;

namespace ITAssetProject.ViewModels.Dashboard
{
    public class AssetByStatus
    {
        [JsonConverter(converterType: typeof(JsonStringEnumConverter))]
        public StatusEnum Status { get; set; }
        public int NumberOfAssets { get; set; }
    }
}
