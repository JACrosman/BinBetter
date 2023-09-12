using BinBetter.Api.Data;
using BinBetter.Api.Security;
using Moq;

namespace BinBetter.Test.Mocks
{
    internal class MockCurrentUserAccessor
    {
        public static Mock<ICurrentUserAccessor> Get()
        {
            var mockRepo = new Mock<ICurrentUserAccessor>();

            mockRepo.Setup(r => r.GetCurrentUserId()).Returns(() => 1);
            mockRepo.Setup(r => r.GetCurrentUsername()).Returns(() => "Fenki");

            return mockRepo;
        }
    }
}
