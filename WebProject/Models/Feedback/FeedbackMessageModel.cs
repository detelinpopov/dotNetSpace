using System;

namespace WebProject.Models.Feedback
{
    public class FeedbackMessageModel
    {
        public string Author { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string Message { get; set; }
    }
}