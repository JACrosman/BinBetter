using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using BinBetter.Api.Errors;
using BinBetter.Api.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BinBetter.Api.Features.Users
{
    public class Create
    {
        public class UserData
        {
            public string? Username { get; set; }

            public string? Email { get; set; }

            public string? Password { get; set; }
        }

        public record Command(UserData User) : IRequest<UserModelEnvelope>;

        public class Handler : IRequestHandler<Command, UserModelEnvelope>
        {
            private readonly IBinBetterRepository _repository;
            private readonly IPasswordHasher _passwordHasher;

            public Handler(IBinBetterRepository repository, IPasswordHasher passwordHasher)
            {
                _repository = repository;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserModelEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                // Check for already existing name
                if (await _repository.Users.FindByUsername(message.User.Username).AnyAsync(cancellationToken))
                {
                    throw new RestException(
                        HttpStatusCode.BadRequest,
                        new { Username = ErrorConstants.IN_USE }
                    );
                }

                // Check for already existing Email
                if (await _repository.Users.FindByEmail(message.User.Email).AnyAsync(cancellationToken))
                {
                    throw new RestException(
                        HttpStatusCode.BadRequest,
                        new { Email = ErrorConstants.IN_USE }
                    );
                }

                // Create new user
                var salt = Guid.NewGuid().ToByteArray();
                var user = new User
                {
                    Username = message.User.Username,
                    Email = message.User.Email,
                    Hash = await _passwordHasher.Hash(
                        message.User.Password ?? throw new InvalidOperationException(),
                        salt
                    ),
                    Salt = salt
                };

                // Persist user
                _repository.Users.Add(user);
                await _repository.SaveAsync();

                UserModel userModel = new UserModel
                {
                    Username = user.Username,
                    Email = user.Email
                };

                return new UserModelEnvelope(userModel);
            }
        }
    }
}
