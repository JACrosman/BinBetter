using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using BinBetter.Api.Security;
using MediatR;

namespace BinBetter.Api.Features.Goals
{
    public class Create
    {
        public class GoalData
        {
            public string? Name { get; set; }

            public string? Description { get; set; }

            public int Frequency { get; set; }
        }

        public record Command(GoalData Goal) : IRequest<GoalModelEnvelope>;

        public class Handler : IRequestHandler<Command, GoalModelEnvelope>
        {
            private readonly IBinBetterRepository _repository;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public Handler(IBinBetterRepository repository, ICurrentUserAccessor currentUserAccessor)
            {
                _repository = repository;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<GoalModelEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var bin = await _repository.Bins.FindByUserIdAsync(_currentUserAccessor.GetCurrentUserId());

                var goal = new Goal
                {
                    Bin = bin,
                    Name = message.Goal.Name,
                    Description = message.Goal.Description,
                    Frequency = message.Goal.Frequency
                };

                _repository.Goals.Add(goal);
                await _repository.SaveAsync();

                return new GoalModelEnvelope(new GoalModel
                {
                    GoalId = goal.GoalId,
                    Name = goal.Name,
                    Description = goal.Description,
                    Frequency = goal.Frequency,
                    BinId =  goal.BinId
                });
            }
        }
    }
}
