using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface IPublicFeedbackService
    {
        IPublicFeedbackMessage CreateMessageObject();

        Task<IEnumerable<IPublicFeedbackMessage>> GetPublicChatMessagesAsync();

        Task SaveMessageAsync(string text, string username);
    }
}