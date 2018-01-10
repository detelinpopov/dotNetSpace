using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface IPublicFeedbackRepository
    {
        IPublicFeedbackMessage CreatePublicChatMessageObject();

        Task<IEnumerable<IPublicFeedbackMessage>> GetPublicChatMessagesAsync();

        Task SaveMessageAsync(string text, string username);
    }
}