using BinBetter.Api.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace BinBetter.Api.Data.Repositories
{
    public class GoalsRepository : Repository<Goal>, IGoalsRepository
    {
        public GoalsRepository(BinBetterContext context) : base(context)
        {
        }

        public override IQueryable<Goal> AsQueryable()
        {
            return base.AsQueryable().AsNoTracking();
        }
    }
}
