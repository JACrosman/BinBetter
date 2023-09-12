namespace BinBetter.Api.Features.Users
{
    public class UserModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }

    public record UserModelEnvelope(UserModel user);
}
