using BinBetter.Api.Data.Domain;
using BinBetter.Api.Data.Repositories;
using MockQueryable.Moq;
using Moq;

namespace BinBetter.Test.Mocks
{
    internal class MockGoalsRepository
    {
        public static Mock<IGoalsRepository> Get()
        {
            var goals = new List<Goal>
            {
                new Goal {
                    GoalId = 1,
                    BinId = 1,
                    Name = "Goal 1",
                    Description = "Goal 1 Description"
                },
                new Goal {
                    GoalId = 2,
                    BinId = 1,
                    Name = "Goal 2",
                    Description = "Goal 2 Description"
                }
            };

            var mockRepo = new Mock<IGoalsRepository>();
            var mockGoals = goals.AsQueryable().BuildMock();

            mockRepo.Setup(r => r.ListAsync()).ReturnsAsync(goals);
            mockRepo.Setup(r => r.QueryableAsync()).Returns(mockGoals);
            mockRepo.Setup(r => r.FindByIdAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken c) => goals.AsEnumerable().FirstOrDefault(x => x.GoalId == id));
            mockRepo.Setup(r => r.Add(It.IsAny<Goal>())).Callback((Goal goal) => goals.Add(goal));
            mockRepo.Setup(r => r.Update(It.IsAny<Goal>())).Callback((Goal goal) => { return; });
            mockRepo.Setup(r => r.Delete(It.IsAny<Goal>())).Callback((Goal goal) => goals.Remove(goal));

            return mockRepo;
        }
    }
}
