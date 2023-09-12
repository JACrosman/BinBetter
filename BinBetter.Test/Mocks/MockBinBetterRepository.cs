using BinBetter.Api.Data;
using Moq;

namespace BinBetter.Test.Mocks
{
    public static class MockBinBetterRepository
    {
        public static Mock<IBinBetterRepository> Get()
        {
            var mockRepo = new Mock<IBinBetterRepository>();
            var mockBinsRepo = MockBinsRepository.Get().Object;
            var mockGoalsRepo = MockGoalsRepository.Get().Object;
            var mockUsersRepository = MockUsersRepository.Get().Object;

            mockRepo.Setup(r => r.Bins).Returns(() => mockBinsRepo);
            mockRepo.Setup(r => r.Goals).Returns(() => mockGoalsRepo);
            mockRepo.Setup(r => r.Users).Returns(() => mockUsersRepository);

            return mockRepo;
        }
    }
}
