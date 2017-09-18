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
    public class QuestionRepository : IQuestionRepository
    {
        public IQuestion CreateQuestion()
        {
            return new Question();
        }

        public async Task<bool> CheckAnswersAsync(int questionId, IEnumerable<int> answersIds)
        {
            using (var context = new QuizContext())
            {
                answersIds = answersIds.Where(a => a > 0);
                if (!answersIds.Any())
                {
                    return false;
                }

                IQuestion question = await context.Questions.Include(nameof(Question.Answers)).FirstOrDefaultAsync(q => q.Id == questionId);
                var correctAnswersIds = question.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToList();
                foreach (var answerId in answersIds)
                {
                    if (!correctAnswersIds.Contains(answerId))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public IAnswer CreateAnswer()
        {
            return new Answer();
        }

        public async Task<IEnumerable<IQuestion>> FindAllAsync()
        {
            using (var context = new QuizContext())
            {
                return await context.Questions.Include(nameof(Question.Answers)).ToListAsync();
            }
        }

        public async Task<IQuestion> FindRandomQuestionAsync(IEnumerable<int> excludeIdsList)
        {
            using (var context = new QuizContext())
            {
                return await context.Questions.Include(nameof(Question.Answers)).FirstOrDefaultAsync(q => !excludeIdsList.Contains(q.Id));
            }
        }

        public virtual async Task<IQuestion> SaveAsync(IQuestion question)
        {
            using (var context = new QuizContext())
            {
                var questionAsEntity = question as Question;
                if (questionAsEntity == null)
                {
                    throw new ArgumentException("Invalid question.");
                }

                context.Questions.Add(questionAsEntity);
                await context.SaveChangesAsync();

                return questionAsEntity;
            }
        }

        public virtual async Task<IQuestion> FindAsync(int id)
        {
            using (var context = new QuizContext())
            {
                return await context.Questions.Include(nameof(Question.Answers)).FirstOrDefaultAsync(x => x.Id == id);
            }
        }
    }
}