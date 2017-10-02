using System.Threading.Tasks;
using Core.Services;
using Interfaces.Sql.Entities;
using Moq;
using NUnit.Framework;
using Shared.Entities;
using Sql.Entities;
using Sql.Repositories;

namespace Tester.Core.Services
{
    [TestFixture]
    public class QuestionServiceFixture
    {
        private Mock<QuestionRepository> _repositoryMock;

        private void SetupRepositoryMock()
        {
            var question = CreateQuestion();
            _repositoryMock = new Mock<QuestionRepository>();
            _repositoryMock.Setup(r => r.SaveAsync(It.IsAny<IQuestion>())).ReturnsAsync(question);
            _repositoryMock.Setup(r => r.FindAsync(It.IsAny<int>())).ReturnsAsync(question);
        }

        private IQuestion CreateQuestion(int index = 1)
        {
            IQuestion question = new Question();
            question.Text = $"Test question {index}";
            question.Category = QuestionCategory.CSharp.ToString();
            return question;
        }

        private bool VerifyQuestion(IQuestion original, IQuestion toCompare)
        {
            Assert.AreEqual(original.Text, toCompare.Text);
            Assert.AreEqual(original.Category, toCompare.Category);

            return true;
        }

        [Test]
        public async Task FindAsync_CallsRepository()
        {
            // Arrange
            SetupRepositoryMock();
            var service = new QuestionService(_repositoryMock.Object);

            // Act
            await service.FindAsync(1);

            // Assert
            _repositoryMock.Verify(r => r.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task SaveAsync_CallsRepository()
        {
            // Arrange
            var question = CreateQuestion();
            SetupRepositoryMock();

            var service = new QuestionService(_repositoryMock.Object);

            // Act
            await service.SaveAsync(question);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<IQuestion>()), Times.Once);
        }

        [Test]
        public async Task SaveAsync_SendsCorrectObjectToRepository()
        {
            // Arrange
            var question = CreateQuestion();
            SetupRepositoryMock();

            var service = new QuestionService(_repositoryMock.Object);

            // Act
            await service.SaveAsync(question);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(It.Is<IQuestion>(q => VerifyQuestion(q, question))), Times.Once);
        }
    }
}