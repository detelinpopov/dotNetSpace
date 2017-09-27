using System.Collections.Generic;

namespace Interfaces.Sql.Entities
{
    public interface IQuestion : IEntity
    {
        string Text { get; set; }

        byte[] Image { get; set; }

        ICollection<IAnswer> Answers { get; set; }

        string Category { get; set; }
    }
}