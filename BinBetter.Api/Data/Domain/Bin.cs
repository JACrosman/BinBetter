using System.Text.Json.Serialization;

namespace BinBetter.Api.Data.Domain
{
    public class Bin
    {
        public int BinId { get; set; }

        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
