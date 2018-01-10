using System;

namespace Interfaces.Sql.Entities
{
    public interface IPublicFeedbackMessage : IEntity
    {
        DateTime CreateDateTime { get; set; }

        string Message { get; set; }

        string SenderName { get; set; }
    }
}