namespace BinBetter.Api.Security
{
    public interface IJwtTokenGenerator
    {
        string CreateToken(string username, int userId);
    }
}
