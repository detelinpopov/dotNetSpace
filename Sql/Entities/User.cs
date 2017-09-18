using System.ComponentModel.DataAnnotations;
using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public class User : Entity, IUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "The password must contain at least 7 symbols")]       
        public string Password { get; set; }
    }
}