using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BinBetter.Api.Features.Goals
{
    public class Get
    {
        public record Query() : IRequest<GoalListEnvelope>;

        public class QueryHandler : IRequestHandler<Query, GoalListEnvelope>
        {
            private readonly IBinBetterRepository _repository;

            public QueryHandler(IBinBetterRepository repository)
            {
                _repository = repository;
            }

            public async Task<GoalListEnvelope> Handle(Query message, CancellationToken cancellation)
            {
                IQueryable<Goal> goalQuery = _repository.Goals.QueryableAsync();

                var goals = await goalQuery.ToListAsync();

                return new GoalListEnvelope { Goals = goals, GoalsCount = goals.Count() };

            }
        }
    }
}
