using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using NUnit.Framework;
using Sql.Context;
using Sql.Entities;
using Sql.Repositories;
using Tester.Sql.Builders;

namespace Tester.Sql.Repositories.IntegrationTests
{
    [TestFixture]
    public class QuestionRepositoryFixture : RepositoryFixture
    {
        [Test]
        public async Task CheckAnswersAsync_ReturnsFalse_WhenNoAnswersPassed()
        {
            // Arrange
            var answersCount = 4;
            var correctAnswerId = 2;
            var question = QuestionBuilder.CreateQuestion().WithAnswers(answersCount, new List<int> {correctAnswerId});
            var repository = new QuestionRepository();

            // Act   
            var savedQuestion = await repository.SaveAsync(question);
            var correctAnswers = await repository.CheckAnswersAsync(savedQuestion.Id, new List<int>());

            // Assert
            Assert.IsFalse(correctAnswers);
        }

        [Test]
        public async Task CheckAnswersAsync_ReturnsFalse_WhenTheAnswersAreNotCorrect()
        {
            // Arrange
            var answersCount = 4;
            var correctAnswerId = 2;
            var question = QuestionBuilder.CreateQuestion().WithAnswers(answersCount, new List<int> {correctAnswerId});
            var repository = new QuestionRepository();

            // Act   
            var savedQuestion = await repository.SaveAsync(question);
            var correctAnswers = await repository.CheckAnswersAsync(savedQuestion.Id, new List<int> {6});

            // Assert
            Assert.IsFalse(correctAnswers);
        }

        [Test]
        public async Task CheckAnswersAsync_ReturnsTrue_WhenTheAnswersAreCorrect()
        {
            // Arrange
            var answersCount = 4;
            var correctAnswerId = 2;
            var question = QuestionBuilder.CreateQuestion().WithAnswers(answersCount, new List<int> {correctAnswerId});
            var repository = new QuestionRepository();

            // Act   
            var savedQuestion = await repository.SaveAsync(question);
            var correctAnswers = await repository.CheckAnswersAsync(savedQuestion.Id, new List<int> {correctAnswerId});

            // Assert
            Assert.IsTrue(correctAnswers);
        }

        [Test]
        public async Task FindAsync_ReturnsQuestion()
        {
            // Arrange
            var question = QuestionBuilder.CreateQuestion();
            var repository = new QuestionRepository();

            // Act        
            var savedQuestion = await repository.SaveAsync(question);

            // Assert
            using (var context = new QuizContext())
            {
                Assert.IsNotNull(savedQuestion);
                var questionFromDatabase = context.Questions.FirstOrDefault(e => e.Id == savedQuestion.Id);
                Assert.IsNotNull(questionFromDatabase);
                Assert.AreEqual(question.Text, savedQuestion.Text);
            }
        }

        [Test]
        public async Task SaveAsync_SavesCorrectAnswerStatus()
        {
            // Arrange
            var answersCount = 4;
            var correctAnswerId = 2;
            var question = QuestionBuilder.CreateQuestion().WithAnswers(answersCount, new List<int> {correctAnswerId});
            var repository = new QuestionRepository();

            // Act   
            var savedQuestion = await repository.SaveAsync(question);

            // Assert
            using (var context = new QuizContext())
            {
                Assert.IsNotNull(savedQuestion);
                var questionFromDatabase = context.Questions.Include(nameof(Question.Answers)).FirstOrDefault(e => e.Id == savedQuestion.Id);
                Assert.IsNotNull(questionFromDatabase);
                Assert.AreEqual(question.Text, questionFromDatabase.Text);
                Assert.IsNotNull(question.Answers);
                Assert.AreEqual(answersCount, question.Answers.Count);
                Assert.AreEqual(1, question.Answers.Count(a => a.IsCorrect));
                Assert.AreEqual(correctAnswerId, question.Answers.First(a => a.IsCorrect).Id);
            }
        }


        [Test]
        public async Task SaveAsync_SavesQuestion()
        {
            // Arrange
            var question = QuestionBuilder.CreateQuestion();
            var repository = new QuestionRepository();

            // Act   
            var savedQuestion = await repository.SaveAsync(question);

            // Assert
            using (var context = new QuizContext())
            {
                Assert.IsNotNull(savedQuestion);
                var questionFromDatabase = context.Questions.FirstOrDefault(e => e.Id == savedQuestion.Id);
                Assert.IsNotNull(questionFromDatabase);
                Assert.AreEqual(question.Text, questionFromDatabase.Text);
            }
        }

        [Test]
        public async Task SaveAsync_SavesQuestionWithAnswers()
        {
            // Arrange
            var answersCount = 4;
            var question = QuestionBuilder.CreateQuestion().WithAnswers(answersCount);
            var repository = new QuestionRepository();

            // Act   
            var savedQuestion = await repository.SaveAsync(question);

            // Assert
            using (var context = new QuizContext())
            {
                Assert.IsNotNull(savedQuestion);
                var questionFromDatabase = context.Questions.Include(nameof(Question.Answers)).FirstOrDefault(e => e.Id == savedQuestion.Id);
                Assert.IsNotNull(questionFromDatabase);
                Assert.AreEqual(question.Text, questionFromDatabase.Text);
                Assert.IsNotNull(question.Answers);
                Assert.AreEqual(answersCount, question.Answers.Count);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SaveAsync_ThrowsException_IfTheQuestionIsNotValid()
        {
            // Arrange
            IQuestion question = null;
            var repository = new QuestionRepository();

            // Act   
            await repository.SaveAsync(question);
        }
    }
}