using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.Quiz
{
    public class FeedbackModel
    {
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        [Required(ErrorMessage = "Please provide email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter text")]
        [MaxLength(500, ErrorMessage = "The message is too long")]
        public string Text { get; set; }
    }
}