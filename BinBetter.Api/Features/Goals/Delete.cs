using BinBetter.Api.Data;
using BinBetter.Api.Errors;
using BinBetter.Api.Security;
using MediatR;
using System.Net;

namespace BinBetter.Api.Features.Goals
{
    public class Delete
    {
        public record Command(int Id) : IRequest;
        public class Handler : IRequestHandler<Command>
        {
            private readonly IBinBetterRepository _repository;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public Handler(IBinBetterRepository repository, ICurrentUserAccessor currentUserAccessor)
            {
                _repository = repository;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task Handle(Command message, CancellationToken cancellationToken)
            {
                var goal = await _repository.Goals.FindByIdAsync(message.Id, cancellationToken);

                if (goal == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Goal = ErrorConstants.NOT_FOUND });
                }


                _repository.Goals.Delete(goal);
                await _repository.SaveAsync();

                await Task.FromResult(Unit.Value);
            }
        }
    }
}