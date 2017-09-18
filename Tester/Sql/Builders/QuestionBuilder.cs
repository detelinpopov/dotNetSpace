using System.Collections.Generic;
using System.Linq;
using Interfaces.Sql.Entities;
using Sql.Entities;

namespace Tester.Sql.Builders
{
    public static class QuestionBuilder
    {
        public static IQuestion CreateQuestion(int index = 1, bool addAnswers = false)
        {
            IQuestion question = new Question();
            question.Text = $"Test Question {index}";
            return question;
        }

        public static IQuestion WithAnswers(this IQuestion question, int count, IEnumerable<int> correctAnswersIds = null)
        {
            IList<IAnswer> answers = new List<IAnswer>();
            for (var i = 0; i < count; i++)
            {
                IAnswer answer = new Answer();
                var answerIndex = i + 1;
                answer.Text = $"Test Answer {answerIndex}";
                int index = i + 1;
                answer.IsCorrect = correctAnswersIds?.Contains(index) ?? i % 2 == 0;
                answers.Add(answer);
            }
            question.Answers = answers;
            return question;
        }
    }
}