using BinBetter.Api.Data;
using BinBetter.Api.Features.Goals;
using BinBetter.Api.Security;
using BinBetter.Test.Mocks;
using Moq;

namespace BinBetter.Test.Features.Goals
{
    public class GoalsUpdateTests
    {
        private readonly Mock<IBinBetterRepository> _mockRepo;
        private readonly Mock<ICurrentUserAccessor> _mockCurrentUserAccessor;

        public GoalsUpdateTests()
        {
            _mockRepo = MockBinBetterRepository.Get();
            _mockCurrentUserAccessor = MockCurrentUserAccessor.Get();
        }

        [Fact]
        public async Task Expect_Create_Goal()
        {
            var command = new Create.Command(
               new Create.GoalData()
               {
                   Name = "Test Goal 123",
                   Description = "Test Goal Description 123"
               }
           );

            var handler = new Create.Handler(_mockRepo.Object, _mockCurrentUserAccessor.Object);

            var result = await handler.Handle(command, CancellationToken.None);
            var goals = await _mockRepo.Object.Goals.ListAsync();

            Assert.NotNull(result);
            Assert.Equal(3, goals.Count());
        }
    }
}
