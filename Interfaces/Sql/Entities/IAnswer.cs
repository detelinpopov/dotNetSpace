namespace Interfaces.Sql.Entities
{
    public interface IAnswer : IEntity
    {
        string Text { get; set; }

        bool IsCorrect { get; set; }

        IQuestion Question { get; set; }
    }
}