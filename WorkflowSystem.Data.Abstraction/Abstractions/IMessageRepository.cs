using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface IMessageRepository : IRepository<Message>
    {
        List<Message> GetUserMessages(Guid userId);
        List<Message> GetUserSentMessages(Guid userId);
        Message GetMessageDetails(Guid id);
        void AddMessage(Message message);
        void AddUserMessage(Message message);
        void UpdateMessage(Message message);
    }
}
