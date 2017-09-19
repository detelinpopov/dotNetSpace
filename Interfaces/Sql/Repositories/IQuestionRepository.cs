using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface IQuestionRepository
    {
        Task<bool> CheckAnswersAsync(int questionId, IEnumerable<int> answersIds);

        IAnswer CreateAnswer();

        IQuestion CreateQuestion();

        Task<IEnumerable<IQuestion>> FindAllAsync();

        Task<IQuestion> FindAsync(int id);

        Task<IQuestion> FindRandomQuestionAsync(IEnumerable<int> excludeIdsList);

        Task<IEnumerable<int>> GetCorrectAnswersIdsAsync(int questionId);

        Task<IQuestion> SaveAsync(IQuestion question);
    }
}