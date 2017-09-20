using System;

namespace WebProject.Models.Quiz
{
    public class QuizCompletedModel
    {
        public int NumberOfCorrectAnswers { get; set; }

        public TimeSpan? TimeSpent { get; set; }

        public string TimeSpentText { get; set; }
    }
}