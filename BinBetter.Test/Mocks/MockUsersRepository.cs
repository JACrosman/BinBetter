using BinBetter.Api.Data.Domain;
using BinBetter.Api.Data.Repositories;
using MockQueryable.Moq;
using Moq;

namespace BinBetter.Test.Mocks
{
    internal class MockUsersRepository
    {
        public static Mock<IUserRepository> Get()
        {
            var users = new List<User>
            {
                new User {
                    UserId = 1,
                    Username = "fenki",
                    Email = "fenki@fenki.com"
                }
            };

            var mockRepo = new Mock<IUserRepository>();
            var mockUsers = users.AsQueryable().BuildMock();

            mockRepo.Setup(r => r.ListAsync()).ReturnsAsync(users);
            mockRepo.Setup(r => r.QueryableAsync()).Returns(mockUsers);
            mockRepo.Setup(r => r.FindByIdAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken c) => users.AsEnumerable().FirstOrDefault(x => x.UserId == id));
            mockRepo.Setup(r => r.FindByUsernameAsync(It.IsAny<string>())).ReturnsAsync((string username) => users.AsEnumerable().FirstOrDefault(x => x.Username == username));
            mockRepo.Setup(r => r.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((string email) => users.AsEnumerable().FirstOrDefault(x => x.Email == email));
            mockRepo.Setup(r => r.Add(It.IsAny<User>())).Callback((User User) => users.Add(User));
            mockRepo.Setup(r => r.Update(It.IsAny<User>())).Callback((User User) => { return; });
            mockRepo.Setup(r => r.Delete(It.IsAny<User>())).Callback((User User) => users.Remove(User));

            return mockRepo;
        }
    }
}
