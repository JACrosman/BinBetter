using BinBetter.Api.Data.Domain;

namespace BinBetter.Api.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> FindByUsername(String username);
        IQueryable<User> FindByEmail(String email);
    }
}
