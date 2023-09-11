using System.Text.Json.Serialization;

namespace BinBetter.Api.Data.Domain
{
    public class Goal
    {
        public int Id { get; set; }

        public int BinId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public int Frequency { get; set; }

        public bool IsInBin { get; set; }

        [JsonIgnore]
        public Bin? Bin{ get; set; }
    }
}
