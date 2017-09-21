using System;

namespace WebProject.Models.Quiz
{
    public class QuizCompletedModel
    {
        public int NumberOfCorrectAnswers { get; set; }

        public TimeSpan? TimeSpent { get; set; }

        public string TimeSpentText { get; set; }

        public int TotalQuestionsCount { get; set; }

        public string UserLevel
        {
            get
            {
                var numberOfMistakes = TotalQuestionsCount - NumberOfCorrectAnswers;
                if (numberOfMistakes <= 2)
                {
                    return Constantns.LevelSuperhero;
                }
                if (numberOfMistakes <= 5)
                {
                    return Constantns.LevelGood;
                }
                if (numberOfMistakes <= 10)
                {
                    return Constantns.LevelMediocre;
                }
                return Constantns.LevelPoor;
            }
        }
    }
}