using BinBetter.Api.Data;
using MediatR;
using BinBetter.Api.Errors;
using System.Net;

namespace BinBetter.Api.Features.Goals
{
    public class GetById
    {
        public record Query(int Id) : IRequest<GoalModelEnvelope>;

        public class QueryHandler : IRequestHandler<Query, GoalModelEnvelope>
        {
            private readonly IBinBetterRepository _repository;

            public QueryHandler(IBinBetterRepository repository)
            {
                _repository = repository;
            }

            public async Task<GoalModelEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var goal = await _repository.Goals.FindByIdAsync(message.Id, cancellationToken);

                if (goal == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Goal = ErrorConstants.NOT_FOUND });
                }

                return new GoalModelEnvelope(new GoalModel
                {
                    GoalId = goal.GoalId,
                    BinId = goal.BinId,
                    Name = goal.Name,
                    Description = goal.Description,
                    Frequency = goal.Frequency
                });
            }
        }
    }
}
