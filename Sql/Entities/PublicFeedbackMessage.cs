using System;
using System.ComponentModel.DataAnnotations;
using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public class PublicFeedbackMessage : Entity, IPublicFeedbackMessage
    {     
        public DateTime CreateDateTime { get; set; }

        [Required]
        public string Message { get; set; }

        public string SenderName { get; set; }
    }
}