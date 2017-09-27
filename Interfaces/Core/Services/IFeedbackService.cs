using System.Threading.Tasks;
using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface IFeedbackService
    {
        IFeedback CreateFeedback();

        Task<IFeedback> SaveAsync(IFeedback feedback);
    }
}