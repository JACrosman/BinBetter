using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using BinBetter.Api.Data.Repositories;
using BinBetter.Api.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BinBetter.Api.Features.Goals
{
    public class Get
    {
        public record Query(): IRequest<List<Goal>>;

        public class QueryHandler : IRequestHandler<Query, List<Goal>>
        {
            private readonly IBinBetterRepository _repository;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public QueryHandler(IBinBetterRepository repository, ICurrentUserAccessor currentUserAccessor)
            {
                _repository = repository;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<List<Goal>> Handle(Query message, CancellationToken cancellation)
            {
                IQueryable<Goal> goalQuery = _repository.Goals.QueryableAsync();

                var goals = await goalQuery.ToListAsync();

                return goals;
            }
        }
    }
}
