using System.Text.Json.Serialization;

namespace BinBetter.Api.Data.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        [JsonIgnore]
        public byte[] Hash { get; set; } = Array.Empty<byte>();

        [JsonIgnore]
        public byte[] Salt { get; set; } = Array.Empty<byte>();
    }
}
