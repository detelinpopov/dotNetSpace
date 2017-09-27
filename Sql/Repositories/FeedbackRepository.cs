using System;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Sql.Context;
using Sql.Entities;

namespace Sql.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public IFeedback CreateFeedback()
        {
            return new Feedback();
        }

        public virtual async Task<IFeedback> SaveAsync(IFeedback feedback)
        {
            using (var context = new QuizContext())
            {
                var feedbackAsEntity = feedback as Feedback;
                if (feedbackAsEntity == null)
                {
                    throw new ArgumentException("Invalid feedback.");
                }

                context.Feedbacks.Add(feedbackAsEntity);
                await context.SaveChangesAsync();

                return feedbackAsEntity;
            }
        }
    }
}