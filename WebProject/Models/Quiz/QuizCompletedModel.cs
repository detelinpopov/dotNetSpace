using System;

namespace WebProject.Models.Quiz
{
    public class QuizCompletedModel
    {
        public int NumberOfCorrectAnswers { get; set; }

        public CompletedQuizResult QuizResult
        {
            get
            {
                var percentOfCorrectAnswers = (NumberOfCorrectAnswers * 100) / TotalQuestionsCount;
                if (percentOfCorrectAnswers == 100)
                {
                    return CompletedQuizResult.Great;
                }
                if (percentOfCorrectAnswers >= 90)
                {
                    return CompletedQuizResult.VeryGood;
                }
                if (percentOfCorrectAnswers >= 70)
                {
                    return CompletedQuizResult.Good;
                }
                return percentOfCorrectAnswers >= 50 ? CompletedQuizResult.Mediocre : CompletedQuizResult.Bad;
            }
        }

        public TimeSpan? TimeSpent { get; set; }

        public string TimeSpentText { get; set; }

        public int TotalQuestionsCount { get; set; }
    }
}