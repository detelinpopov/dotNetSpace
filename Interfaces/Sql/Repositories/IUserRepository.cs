using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using Shared;

namespace Interfaces.Sql.Repositories
{
    public interface IUserRepository
    {
        IUser CreateUser();

        Task<IUser> FindAsync(string name);

        Task<ValidationResult> RegisterAsync(string name, string password);
    }
}