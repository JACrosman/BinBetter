using AutoMapper;
using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using BinBetter.Api.Errors;
using BinBetter.Api.Security;
using FluentValidation;
using MediatR;
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

        public class UserDataValidator : AbstractValidator<UserData>
        {
            public UserDataValidator()
            {
                RuleFor(x => x.Username).NotNull().NotEmpty();
                RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6);
            }
        }

        public record Command(UserData User) : IRequest<UserModelEnvelope>;

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).NotNull().SetValidator(new UserDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, UserModelEnvelope>
        {
            private readonly IBinBetterRepository _repository;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IMapper _mapper;

            public Handler(IBinBetterRepository repository, IPasswordHasher passwordHasher, IMapper mapper)
            {
                _repository = repository;
                _passwordHasher = passwordHasher;
                _mapper = mapper;
            }

            public async Task<UserModelEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                // Check for already existing name
                if (await _repository.Users.FindByUsernameAsync(message.User.Username) != null)
                {
                    throw new RestException(
                        HttpStatusCode.BadRequest,
                        new { Username = ErrorConstants.IN_USE }
                    );
                }

                // Check for already existing Email
                if (await _repository.Users.FindByEmailAsync(message.User.Email) != null)
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

                return new UserModelEnvelope(_mapper.Map<User, UserModel>(user));
            }
        }
    }
}
