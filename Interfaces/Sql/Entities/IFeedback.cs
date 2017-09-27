namespace Interfaces.Sql.Entities
{
    public interface IFeedback : IEntity
    {
        string Email { get; set; }

        string Text { get; set; }
    }
}