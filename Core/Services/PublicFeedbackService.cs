using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;

namespace Core.Services
{
    public class PublicFeedbackService : IPublicFeedbackService
    {
        private readonly IPublicFeedbackRepository _repository;

        public PublicFeedbackService(IPublicFeedbackRepository repository)
        {
            _repository = repository;
        }

        public IPublicFeedbackMessage CreateMessageObject()
        {
            return _repository.CreatePublicChatMessageObject();
        }

        public async Task<IEnumerable<IPublicFeedbackMessage>> GetPublicChatMessagesAsync()
        {
            return await _repository.GetPublicChatMessagesAsync();
        }

        public async Task SaveMessageAsync(string text, string username)
        {
            await _repository.SaveMessageAsync(text, username);
        }
    }
}