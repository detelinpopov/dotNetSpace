using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using Shared.Entities;

namespace Interfaces.Core.Services
{
    public interface IQuestionService
    {
        Task<bool> CheckAnswersAsync(int questionId, IEnumerable<int> answersIds);

        IAnswer CreateAnswer();

        IQuestion CreateQuestion();

        Task<IQuestion> FindAsync(int id);

        Task<IQuestion> FindRandomQuestionAsync(QuestionCategory category, IEnumerable<int> excludeIdsList);

        Task<IEnumerable<int>> GetCorrectAnswersIdsAsync(int questionId);

        Task<IQuestion> SaveAsync(IQuestion question);
    }
}