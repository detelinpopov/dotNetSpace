using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Sql.Context;
using Sql.Repositories;

namespace Tester.Sql.Repositories.IntegrationTests
{
    [TestFixture]
    public class UserRepositoryFixture : RepositoryFixture
    {
        [Test]
        public async Task RegisterAsync_CreatesUser()
        {
            // Arrange
            var name = "Test user";
            var password = "testP@ssword";
            var repository = new UserRepository();

            // Act        
            await repository.RegisterAsync(name, password);

            // Assert
            using (var context = new QuizContext())
            {
                var savedUser = context.Users.FirstOrDefault(u => u.Name == name);
                Assert.IsNotNull(savedUser);
            }
        }
    }
}