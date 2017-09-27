using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Sql.Context;
using Sql.Entities;
using Sql.Repositories;

namespace Tester.Sql.Repositories.IntegrationTests
{
    [TestFixture]
    public class FeedbackRepositoryFixture : RepositoryFixture
    {
        [Test]
        public async Task SaveAsync_SavesFeedback()
        {
            // Arrange
            var feedback = new Feedback
            {
                Email = "test@test.com",
                Text = "Test Feedback Text"
            };
            var repository = new FeedbackRepository();

            // Act   
            var savedFeedback = await repository.SaveAsync(feedback);

            // Assert
            using (var context = new QuizContext())
            {
                Assert.IsNotNull(feedback);
                var feedbackFromDatabase = context.Feedbacks.FirstOrDefault(e => e.Id == savedFeedback.Id);
                Assert.IsNotNull(feedbackFromDatabase);
                Assert.AreEqual(feedback.Text, feedbackFromDatabase.Text);
            }
        }
    }
}