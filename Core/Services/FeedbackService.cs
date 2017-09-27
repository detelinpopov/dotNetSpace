using System.Threading.Tasks;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;

namespace Core.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackService(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        public IFeedback CreateFeedback()
        {
            return _repository.CreateFeedback();
        }

        public async Task<IFeedback> SaveAsync(IFeedback feedback)
        {
            return await _repository.SaveAsync(feedback);
        }
    }
}