using System.Threading.Tasks;
using Core.Services;
using Interfaces.Sql.Entities;
using Moq;
using NUnit.Framework;
using Sql.Entities;
using Sql.Repositories;

namespace Tester.Core.Services
{
    [TestFixture]
    public class EmployeeServiceFixture
    {
        private Mock<QuestionRepository> _repositoryMock;

        private void SetupRepositoryMock()
        {
            //IQuestion employee = CreateEmployee();
            _repositoryMock = new Mock<QuestionRepository>();
            //_repositoryMock.Setup(r => r.SaveAsync(It.IsAny<IQuestion>())).ReturnsAsync(employee);
            //_repositoryMock.Setup(r => r.FindAsync(It.IsAny<int>())).ReturnsAsync(employee);
        }

        //private IQuestion CreateEmployee(int index = 1)
        //{
        //    IQuestion employee = new Question();
        //    employee.Name = "TestName " + index;
        //    employee.Email = "test@test.com";
        //    employee.ManagerId = index;
        //    return employee;
        //}

        //private bool VerifyEmployee(IQuestion original, IQuestion toCompare)
        //{
        //    Assert.AreEqual(original.Name, toCompare.Name);
        //    Assert.AreEqual(original.Email, toCompare.Email);
        //    Assert.AreEqual(original.ManagerId, toCompare.ManagerId);

        //    return true;
        //}

        [Test]
        public async Task FindAsync_CallsRepository()
        {
            // Arrange
            SetupRepositoryMock();
            QuestionService service = new QuestionService(_repositoryMock.Object);

            // Act
            await service.FindAsync(1);

            // Assert
            _repositoryMock.Verify(r => r.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task SaveAsync_CallsRepository()
        {
            // Arrange
            //IQuestion employee = CreateEmployee();
            SetupRepositoryMock();

            QuestionService service = new QuestionService(_repositoryMock.Object);

            // Act
           // await service.SaveAsync(employee);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<IQuestion>()), Times.Once);
        }

        [Test]
        public async Task SaveAsync_SendsCorrectObjectToRepository()
        {
            // Arrange
           // IQuestion employee = CreateEmployee();
            SetupRepositoryMock();

            QuestionService service = new QuestionService(_repositoryMock.Object);

            // Act
           // await service.SaveAsync(employee);

            // Assert
            //_repositoryMock.Verify(r => r.SaveAsync(It.Is<IQuestion>(e => VerifyEmployee(e, employee))), Times.Once);
        }
    }
}