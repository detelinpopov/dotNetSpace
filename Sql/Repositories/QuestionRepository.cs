﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Shared.Entities;
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

        public async Task<bool> ExistingQuestionsOfCategoryAsync(QuestionCategory category)
        {
            using (var context = new QuizContext())
            {
                return await context.Questions.AnyAsync(c => c.Category == category.ToString());
            }
        }

        public async Task<bool> CheckAnswersAsync(int questionId, IEnumerable<int> answersIds)
        {
            using (var context = new QuizContext())
            {
                IQuestion question = await context.Questions.Include(nameof(Question.Answers)).FirstOrDefaultAsync(q => q.Id == questionId);
                if (question == null || answersIds == null || !answersIds.Any())
                {
                    return false;
                }

                var correctAnswersIds = question.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToList();
                if (correctAnswersIds.Count != answersIds.Count())
                {
                    return false;
                }

                return answersIds.All(answerId => correctAnswersIds.Contains(answerId));
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

        public async Task<IQuestion> FindRandomQuestionAsync(QuestionCategory category, IEnumerable<int> excludeIdsList)
        {
            using (var context = new QuizContext())
            {
                return await context.Questions.Include(nameof(Question.Answers)).OrderBy(r => Guid.NewGuid()).FirstOrDefaultAsync(q => q.Category == category.ToString() && !excludeIdsList.Contains(q.Id));
            }
        }

        public async Task<IEnumerable<int>> GetCorrectAnswersIdsAsync(int questionId)
        {
            using (var context = new QuizContext())
            {
                var correctAnswersIds = await context.Answers.Where(a => a.QuestionId == questionId && a.IsCorrect).Select(a => a.Id).ToListAsync();
                return correctAnswersIds;
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