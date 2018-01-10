using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.Feedback
{
    public class FeedbackModel
    {
        [Required(ErrorMessage = "Please input text")]
        [MaxLength(1000, ErrorMessage = "The text length cannot exceed 1000 symbols")]
        public string Message { get; set; }

        public List<FeedbackMessageModel> MessageModels { get; } = new List<FeedbackMessageModel>();

        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(40, ErrorMessage = "The text length cannot exceed 40 symbols")]
        public string SenderName { get; set; }
    }
}