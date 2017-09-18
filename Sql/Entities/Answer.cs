using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public class Answer : Entity, IAnswer
    {
        public virtual Question Question { get; set; }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        IQuestion IAnswer.Question
        {
            get { return Question; }
            set { Question = (Question) value; }
        }
    }
}