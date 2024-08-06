using System.Text.Json.Serialization;

namespace ITAssetProject.Models
{
    public class ITAsset
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public DateTime WarrantyExpirationDate { get; set; }

        [JsonConverter(converterType: typeof(JsonStringEnumConverter))]
        public StatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum StatusEnum
    {
        New,
        InUse,
        Damaged,
        Dispose
    }
}
