using System.Collections.Generic;

namespace WebProject.Models.Quiz
{
    public class QuestionModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public IList<AnswerModel> Answers { get; } = new List<AnswerModel>();

        public int Number { get; set; }

        public int TotalQuestionsCount { get; set; } = 20;
    }
}