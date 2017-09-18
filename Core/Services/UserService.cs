using System.Threading.Tasks;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Shared;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IUser CreateUser()
        {
            return _userRepository.CreateUser();
        }

        public async Task<bool> IsValidUser(string name, string password)
        {
            var user = await _userRepository.FindAsync(name);
            if (user == null)
            {
                return false;
            }
           
            return PasswordHasher.VerifyHashedPassword(user.Password, password);
        }

        public async Task<ValidationResult> RegisterAsync(string name, string password)
        {
            return await _userRepository.RegisterAsync(name, password);
        }
    }
}