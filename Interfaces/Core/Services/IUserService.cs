using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using Shared;

namespace Interfaces.Core.Services
{
    public interface IUserService
    {
        IUser CreateUser();

        Task<bool> IsValidUser(string name, string password);

        Task<ValidationResult> RegisterAsync(string name, string password);
    }
}