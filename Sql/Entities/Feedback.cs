using System.ComponentModel.DataAnnotations;
using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public class Feedback : Entity, IFeedback
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter text.")]
        [MaxLength(500, ErrorMessage = "The message is too long.")]
        public string Text { get; set; }
    }
}