namespace BinBetter.Api.Security
{
    public interface IPasswordHasher
    {
        Task<byte[]> Hash(string password, byte[] salt);
    }
}
