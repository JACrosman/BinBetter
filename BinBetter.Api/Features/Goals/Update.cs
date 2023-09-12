using BinBetter.Api.Data;
using BinBetter.Api.Errors;
using BinBetter.Api.Security;
using MediatR;
using System.Net;

namespace BinBetter.Api.Features.Goals
{
    public class Update
    {
        public class GoalData
        {
            public string? Name { get; set; }

            public string? Description { get; set; }

            public int Frequency { get; set; }
        }

        public record Command(int Id, Model Model) : IRequest<GoalEnvelope>;
        public record Model(GoalData Goal);

        public class Handler : IRequestHandler<Command, GoalEnvelope>
        {
            private readonly IBinBetterRepository _repository;

            public Handler(IBinBetterRepository repository, ICurrentUserAccessor currentUserAccessor)
            {
                _repository = repository;
            }

            public async Task<GoalEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var goal = await _repository.Goals.FindByIdAsync(message.Id, cancellationToken);

                if (goal == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Goal = ErrorConstants.NOT_FOUND });
                }

                // Update goal props
                goal.Name = message.Model.Goal.Name;
                goal.Description = message.Model.Goal.Description;
                goal.Frequency = message.Model.Goal.Frequency;

                _repository.Goals.Update(goal);
                await _repository.SaveAsync();

                return new GoalEnvelope(goal);
            }
        }
    }
}
