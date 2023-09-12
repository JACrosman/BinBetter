using BinBetter.Api.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace BinBetter.Api.Data.Repositories
{
    public class BinsRepository : Repository<Bin>, IBinsRepository
    {
        public BinsRepository(BinBetterContext context) : base(context)
        {
        }

        public override IQueryable<Bin> QueryableAsync()
        {
            return base.QueryableAsync().AsNoTracking();
        }

        public Task<Bin?> FindByUserIdAsync(int userId)
        {
            return _context.Bins.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
