using BinBetter.Api.Data.Domain;
using BinBetter.Api.Data.Repositories;
using MockQueryable.Moq;
using Moq;

namespace BinBetter.Test.Mocks
{
    internal class MockBinsRepository
    {
        public static Mock<IBinsRepository> Get()
        {
            var bins = new List<Bin>
            {
                new Bin {
                    BinId = 1,
                    Name = "Bin 1",
                    Description = "Bin 1 Description"
                },
                new Bin {
                    BinId = 2,
                    Name = "Bin 2",
                    Description = "Bin 2 Description"
                }
            };

            var mockRepo = new Mock<IBinsRepository>();
            var mockBins = bins.AsQueryable().BuildMock();

            mockRepo.Setup(r => r.ListAsync()).ReturnsAsync(() => bins);
            mockRepo.Setup(r => r.QueryableAsync()).Returns(mockBins);
            mockRepo.Setup(r => r.FindByIdAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken c) => bins.AsEnumerable().FirstOrDefault(x => x.BinId == id));
            mockRepo.Setup(r => r.Add(It.IsAny<Bin>())).Callback((Bin bin) =>
            {
                bins.Add(bin);
            });

            return mockRepo;
        }
    }
}
