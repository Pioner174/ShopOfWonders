using System.Text.Json.Serialization;

namespace SOW.DataModels
{
    public class Tag
    {
        public long TagId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Product>? Products { get; set; }

        [JsonIgnore]
        public IEnumerable<ProductsTags>? ProductsTags { get; set; }
    }
}
