using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Shared.Entities;

namespace Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IQuestion> FindRandomQuestionAsync(QuestionCategory category, IEnumerable<int> excludeIdsList)
        {
            return await _repository.FindRandomQuestionAsync(category, excludeIdsList);
        }

        public async Task<IEnumerable<int>> GetCorrectAnswersIdsAsync(int questionId)
        {
            return await _repository.GetCorrectAnswersIdsAsync(questionId);
        }

        public async Task<IQuestion> SaveAsync(IQuestion question)
        {
            return await _repository.SaveAsync(question);
        }

        public IQuestion CreateQuestion()
        {
            return _repository.CreateQuestion();
        }

        public async Task<bool> ExistingQuestionsOfCategoryAsync(QuestionCategory category)
        {
            return await _repository.ExistingQuestionsOfCategoryAsync(category);
        }

        public async Task<bool> CheckAnswersAsync(int questionId, IEnumerable<int> answersIds)
        {
            return await _repository.CheckAnswersAsync(questionId, answersIds);
        }

        public IAnswer CreateAnswer()
        {
            return _repository.CreateAnswer();
        }

        public async Task<IQuestion> FindAsync(int id)
        {
            return await _repository.FindAsync(id);
        }
    }
}