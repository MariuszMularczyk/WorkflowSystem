using WorkflowSystem.Domain;
using WorkflowSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public interface IMessageService
    {
        object GetUserMessages(Guid userId);
        object GetUserSentMessages(Guid userId);
        Message GetMessageDetails(Guid id);
        void AddMessage(SendMessageDto model);
        void MarkMessageAsRead(Guid id);
        void DeleteMessage(Guid id);
    }
}
