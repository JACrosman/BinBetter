using BinBetter.Api.Data.Domain;

namespace BinBetter.Api.Data.Repositories
{
    public interface IBinsRepository : IRepository<Bin>
    {
        Task<Bin?> FindByUserIdAsync(int userId);
    }
}
