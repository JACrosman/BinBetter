using BinBetter.Api.Data;
using BinBetter.Api.Errors;
using BinBetter.Api.Features.Goals;
using BinBetter.Api.Security;
using BinBetter.Test.Mocks;
using Moq;
using System.Net;

namespace BinBetter.Test.Features.Goals
{
    public class GoalsDeleteTests
    {
        private readonly Mock<IBinBetterRepository> _mockRepo;
        private readonly Mock<ICurrentUserAccessor> _mockCurrentUserAccessor;

        public GoalsDeleteTests()
        {
            _mockRepo = MockBinBetterRepository.Get();
            _mockCurrentUserAccessor = MockCurrentUserAccessor.Get();
        }

        [Fact]
        public async Task Expect_Delete_Goal()
        {
            var command = new Delete.Command(1);

            var handler = new Delete.Handler(_mockRepo.Object, _mockCurrentUserAccessor.Object);

            await handler.Handle(command, CancellationToken.None);
            var goals = await _mockRepo.Object.Goals.ListAsync();

            Assert.Single(goals);
        }

        [Fact]
        public async Task Expect_Delete_Goal_NotFound()
        {
            var command = new Delete.Command(-1);

            var handler = new Delete.Handler(_mockRepo.Object, _mockCurrentUserAccessor.Object);

            var ex = await Assert.ThrowsAsync<RestException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal(HttpStatusCode.NotFound, ex.Code);
        }
    }
}
