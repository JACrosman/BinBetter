using AutoMapper;
using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using BinBetter.Api.Data.Repositories;
using BinBetter.Api.Errors;
using BinBetter.Api.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BinBetter.Api.Features.Users
{
    public class Authenticate
    {
        public record Command(string Email, string Password) : IRequest<UserModelEnvelope>;

        public class CommandHandler : IRequestHandler<Command, UserModelEnvelope>
        {

            private readonly IBinBetterRepository _repository;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IMapper _mapper;

            public CommandHandler(IBinBetterRepository repository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IMapper mapper)
            {
                _repository = repository;
                _jwtTokenGenerator = jwtTokenGenerator;
                _passwordHasher = passwordHasher;
                _mapper = mapper;
            }

            public async Task<UserModelEnvelope> Handle(Command command, CancellationToken cancellation)
            {
                var user = await _repository.Users.FindByEmailAsync(command.Email);

                // Validate user exists
                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new { Error = "Username does not exist" });
                }

                // Validate user password
                if (!user.Hash.SequenceEqual(
                    await _passwordHasher.Hash(command.Password ?? throw new InvalidOperationException(), user.Salt)))
                {
                    throw new RestException(
                       HttpStatusCode.Unauthorized,
                       new { Error = "Invalid password." }
                   );
                }

                var userModel = _mapper.Map<User, UserModel>(user);
                userModel.Token = _jwtTokenGenerator.CreateToken(
                    user.Username ?? throw new InvalidOperationException(),
                    user.UserId
                );

                return new UserModelEnvelope(userModel);
            }
        }
    }
}
