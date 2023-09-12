
using BinBetter.Api.Data;
using BinBetter.Api.Errors;
using BinBetter.Api.Features.Goals;
using BinBetter.Test.Mocks;
using Moq;
using System.Net;

namespace BinBetter.Test.Features.Goals
{
    public class GoalsGetTests
    {
        private readonly Mock<IBinBetterRepository> _mockRepo;

        public GoalsGetTests()
        {
            _mockRepo = MockBinBetterRepository.Get();
        }

        [Fact]
        public async Task Expect_GetAllGoals_To_Exist()
        {
            var query = new Get.Query();

            var handler = new Get.QueryHandler(_mockRepo.Object);

            var goals = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(goals.Goals);
            Assert.Equal(2, goals.GoalsCount);
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

        [Fact]
        public async Task Expect_GetGoalById_To_Not_Exist()
        {
            var query = new GetById.Query(-1);
            var handler = new GetById.QueryHandler(_mockRepo.Object);

            var ex = await Assert.ThrowsAsync<RestException>(() => handler.Handle(query, CancellationToken.None));

            Assert.Equal(HttpStatusCode.NotFound, ex.Code);
        }
    }
}
