using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public class Feedback : Entity, IFeedback
    {
        public string Email { get; set; }

        public string Text { get; set; }
    }
}