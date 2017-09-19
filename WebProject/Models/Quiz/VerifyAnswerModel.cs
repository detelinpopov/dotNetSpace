using WebProject.Enums;

namespace WebProject.Models.Quiz
{
    public class VerifyAnswerModel
    {
        public int QuestionId { get; set; }

        public string[] CorrectAnswersIds { get; set; }

        public string AnswerResult { get; set; }
    }
}