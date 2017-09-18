using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface IQuestionService
    {
        Task<bool> CheckAnswersAsync(int questionId, IEnumerable<int> answersIds);

        IAnswer CreateAnswer();

        IQuestion CreateQuestion();

        Task<IQuestion> FindAsync(int id);

        Task<IQuestion> FindRandomQuestionAsync(IEnumerable<int> excludeIdsList);

        Task<IQuestion> SaveAsync(IQuestion question);
    }
}