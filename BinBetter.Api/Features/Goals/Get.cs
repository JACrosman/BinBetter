using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using BinBetter.Api.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BinBetter.Api.Features.Goals
{
    public class Get
    {
        public record Query(): IRequest<List<Goal>>;

        public class QueryHandler : IRequestHandler<Query, List<Goal>>
        {
            private readonly IGoalsRepository _goalsRepository;

            public QueryHandler(IGoalsRepository goalsRepository)
            {
                _goalsRepository = goalsRepository;
            }

            /// <summary>
            /// Get all goals based on query message
            /// </summary>
            /// <param name="message">query message params</param>
            /// <param name="cancellation">cancellation token</param>
            /// <returns>List of goals</returns>
            public async Task<List<Goal>> Handle(Query message, CancellationToken cancellation)
            {
                IQueryable<Goal> goalQuery = _goalsRepository.AsQueryable();

                var goals = await goalQuery.ToListAsync();

                return goals;
            }
        }

    }
}
