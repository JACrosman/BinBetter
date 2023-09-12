using BinBetter.Api.Data;
using BinBetter.Api.Errors;
using BinBetter.Api.Features.Goals;
using BinBetter.Api.Security;
using BinBetter.Test.Mocks;
using Moq;
using System.Net;

namespace BinBetter.Test.Features.Goals
{
    public class GoalsCreateTests
    {
        private readonly Mock<IBinBetterRepository> _mockRepo;
        private readonly Mock<ICurrentUserAccessor> _mockCurrentUserAccessor;

        public GoalsCreateTests()
        {
            _mockRepo = MockBinBetterRepository.Get();
            _mockCurrentUserAccessor = MockCurrentUserAccessor.Get();
        }

        [Fact]
        public async Task Expect_Update_Goal()
        {
            var goalData = new Update.GoalData()
            {
                Name = "Test Goal Updated",
                Description = "Test Goal Description Updated",
                Frequency = 99
            };
            var command = new Update.Command(1, new Update.Model(goalData));

            var handler = new Update.Handler(_mockRepo.Object, _mockCurrentUserAccessor.Object);
            var goal = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(goal);
            Assert.Equal(goalData.Name, goal.Goal.Name);
            Assert.Equal(goalData.Description, goal.Goal.Description);
            Assert.Equal(goalData.Frequency, goal.Goal.Frequency);
        }

        [Fact]
        public async Task Expect_Update_Goal_To_Not_Exist()
        {
            var goalData = new Update.GoalData()
            {
                Name = "Test Goal Updated",
                Description = "Test Goal Description Updated",
                Frequency = 99
            };
            var command = new Update.Command(-1, new Update.Model(goalData));
            var handler = new Update.Handler(_mockRepo.Object, _mockCurrentUserAccessor.Object);

            var ex = await Assert.ThrowsAsync<RestException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal(HttpStatusCode.NotFound, ex.Code);
        }
    }
}
