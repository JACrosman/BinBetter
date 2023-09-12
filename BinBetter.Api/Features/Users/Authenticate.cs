using BinBetter.Api.Data;
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
        public record Command(string Username, string Password) : IRequest<UserModel>;

        public class CommandHandler : IRequestHandler<Command, UserModel>
        {

            private readonly IBinBetterRepository _repository;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private readonly IPasswordHasher _passwordHasher;

            public CommandHandler(IBinBetterRepository repository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
            {
                _repository = repository;
                _jwtTokenGenerator = jwtTokenGenerator;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserModel> Handle(Command command, CancellationToken cancellation)
            {
                var user = await _repository.Users.FindByUsernameAsync(command.Username);

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

                var token = _jwtTokenGenerator.CreateToken(
                    user.Username ?? throw new InvalidOperationException(),
                    user.UserId
                );
                UserModel userModel = new UserModel
                {
                    Username = command.Username,
                    Token = token
                };

                return userModel;
            }
        }
    }
}
