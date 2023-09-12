using BinBetter.Api.Data.Domain;

namespace BinBetter.Api.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByUsernameAsync(String username);
        Task<User?> FindByEmailAsync(String email);
    }
}
