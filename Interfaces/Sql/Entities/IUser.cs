namespace Interfaces.Sql.Entities
{
    public interface IUser : IEntity
    {
         string Name { get; set; }
      
         string Password { get; set; }
    }
}