using AutoMapper;
using BinBetter.Api.Data;
using BinBetter.Api.Errors;
using BinBetter.Api.Features.Users;
using BinBetter.Api.Security;
using BinBetter.Test.Mocks;
using Moq;
using System.Net;

namespace BinBetter.Test.Features.Users
{
    public class UsersCreateTests
    {
        private readonly Mock<IBinBetterRepository> _mockRepo;
        private readonly PasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UsersCreateTests()
        {
            _mockRepo = MockBinBetterRepository.Get();
            _passwordHasher = new PasswordHasher();
            _mapper = MockMappingProfiles.Get();
        }

        [Fact]
        public async Task Expect_Create_User()
        {
            var command = new Create.Command(
               new Create.UserData()
               {
                   Username = "daisy",
                   Password = "mae",
                   Email = "daisy@mae.com"
               }
           );

            var handler = new Create.Handler(_mockRepo.Object, _passwordHasher, _mapper);

            var user = await handler.Handle(command, CancellationToken.None);
            var users = await _mockRepo.Object.Users.ListAsync();

            Assert.NotNull(user);
            Assert.Equal(2, users.Count());
        }

        [Fact]
        public async Task Expect_Create_User_Username_Already_Exists()
        {
            var command = new Create.Command(
               new Create.UserData()
               {
                   Username = "fenki",
                   Password = "fenki",
                   Email = "fenki@fenki.com"
               }
           );

            var handler = new Create.Handler(_mockRepo.Object, _passwordHasher, _mapper);
            var ex = await Assert.ThrowsAsync<RestException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal(HttpStatusCode.BadRequest, ex.Code);
        }

        [Fact]
        public async Task Expect_Create_User_Email_Already_Exists()
        {
            var command = new Create.Command(
               new Create.UserData()
               {
                   Username = "daisy",
                   Password = "fenki",
                   Email = "fenki@fenki.com"
               }
           );

            var handler = new Create.Handler(_mockRepo.Object, _passwordHasher, _mapper);
            var ex = await Assert.ThrowsAsync<RestException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal(HttpStatusCode.BadRequest, ex.Code);
        }
    }
}
