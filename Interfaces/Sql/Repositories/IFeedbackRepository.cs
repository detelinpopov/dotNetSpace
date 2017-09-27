using System.Threading.Tasks;
using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface IFeedbackRepository
    {
        IFeedback CreateFeedback();

        Task<IFeedback> SaveAsync(IFeedback feedback);
    }
}