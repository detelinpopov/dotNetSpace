using System.Collections.Generic;

namespace WebProject.Models.Quiz
{
    public class ResponseModel
    {
        public int QuestionId { get; set; }

        public IList<int> AnswerIds { get; } = new List<int>();
    }
}