
using BinBetter.Api.Data;
using BinBetter.Api.Features.Goals;
using BinBetter.Api.Security;
using BinBetter.Test.Mocks;
using Moq;

namespace BinBetter.Test.Features.Goals
{
    public class GoalsGetTests
    {
        private readonly Mock<IBinBetterRepository> _mockRepo;
        private readonly Mock<ICurrentUserAccessor> _mockCurrentUserAccessor;

        public GoalsGetTests()
        {
            _mockRepo = MockBinBetterRepository.Get();
            _mockCurrentUserAccessor = MockCurrentUserAccessor.Get();
        }

        [Fact]
        public async Task Expect_GetAllGoals_To_Exist()
        {
            var query = new Get.Query();

            var handler = new Get.QueryHandler(_mockRepo.Object, _mockCurrentUserAccessor.Object);

            var goals = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(goals);
            Assert.Equal(2, goals.Count);
        }

        [Fact]
        public async Task Expect_GetGoalById_To_Exist()
        {
            var query = new GetById.Query(1);

            var handler = new GetById.QueryHandler(_mockRepo.Object);

            var goal = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(goal);
            Assert.NotNull(goal.Goal);
            Assert.NotNull(goal.Goal.Name);
            Assert.NotEmpty(goal.Goal.Name);
        }
    }
}
