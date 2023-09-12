using BinBetter.Api.Data;
using BinBetter.Api.Data.Domain;
using BinBetter.Api.Data.Repositories;
using Moq;


namespace BinBetter.Test.Mocks
{
    public static class MockBinBetterRepository
    {
        public static Mock<IBinBetterRepository> Get()
        {
            var mockRepo = new Mock<IBinBetterRepository>();

            mockRepo.Setup(r => r.Goals).Returns(() => MockGoalsRepository.Get().Object);
            mockRepo.Setup(r => r.Bins).Returns(() => MockBinsRepository.Get().Object);

            return mockRepo;
        }
    }
}
