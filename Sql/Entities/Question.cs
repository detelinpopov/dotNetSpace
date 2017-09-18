using System.Collections.Generic;
using System.Linq;
using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public class Question : Entity, IQuestion
    {
        public virtual ICollection<Answer> Answers { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        ICollection<IAnswer> IQuestion.Answers
        {
            get { return Answers.Select(a => (IAnswer)a).ToList(); }
            set { Answers = value.ToList().ConvertAll(a => (Answer) a); }
        }
    }
}