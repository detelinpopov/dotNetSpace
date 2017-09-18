using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Shared;
using Sql.Context;
using Sql.Entities;

namespace Sql.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IUser CreateUser()
        {
            return new User();
        }

        public async Task<IUser> FindAsync(string name)
        {
            using (var context = new QuizContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Name == name);
                return user;
            }
        }

        public async Task<ValidationResult> RegisterAsync(string name, string password)
        {
            using (var context = new QuizContext())
            {
                var validationResult = new ValidationResult();

                var existingUserWithSameName = await context.Users.FirstOrDefaultAsync(u => u.Name.Trim().ToLower() == name.Trim().ToLower());
                if (existingUserWithSameName != null)
                {
                    validationResult.AddError("User with the same name already exists.");
                    return validationResult;
                }

                var user = new User {Name = name};
                var passwordHashed = PasswordHasher.HashPassword(password);
                user.Password = passwordHashed;
                context.Users.Add(user);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    validationResult.AddError(ex.Message);
                }
                return validationResult;
            }
        }
    }
}