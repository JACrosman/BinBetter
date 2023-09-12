using BinBetter.Api.Data.Repositories;

namespace BinBetter.Api.Data
{
    public interface IBinBetterRepository
    {
        IUserRepository Users { get; }
        IBinsRepository Bins { get; }
        IGoalsRepository Goals { get; }
        Task<int> SaveAsync();
    }
}
