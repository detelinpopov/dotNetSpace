using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Sql.Context;
using Sql.Entities;

namespace Sql.Repositories
{
    public class PublicFeedbackRepository : IPublicFeedbackRepository
    {
        public IPublicFeedbackMessage CreatePublicChatMessageObject()
        {
            return new PublicFeedbackMessage();
        }

        public async Task<IEnumerable<IPublicFeedbackMessage>> GetPublicChatMessagesAsync()
        {
            using (var context = new QuizContext())
            {
                var count = context.FeedbackMessages.Count();
                var messagesToSkip = count > 300 ? 300 - count : 0;
                var messagesToTake = count > 300 ? 300 : count;
                var messages = await context.FeedbackMessages.OrderByDescending(m => m.CreateDateTime).Skip(messagesToSkip).Take(messagesToTake).ToListAsync();
                return messages;
            }
        }

        public async Task SaveMessageAsync(string text, string username)
        {
            using (var context = new QuizContext())
            {
                var message = new PublicFeedbackMessage
                {
                    Message = text,
                    SenderName = username,
                    CreateDateTime = DateTime.UtcNow
                };
                context.FeedbackMessages.Add(message);

                await context.SaveChangesAsync();
            }
        }
    }
}