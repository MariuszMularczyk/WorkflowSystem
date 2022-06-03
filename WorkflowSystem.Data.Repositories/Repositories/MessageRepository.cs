using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.EntityFramework;

namespace WorkflowSystem.Data
{
    public class MessageRepository : Repository<Message, MainDatabaseContext>, IMessageRepository
    {
        public MessageRepository(MainDatabaseContext context) : base(context)
        { }


        public List<Message> GetUserMessages(Guid userId)
        {
            return Context.Messages.Include(x => x.ToUser).ToList();
        }

        public List<Message> GetUserSentMessages(Guid userId)
        {
            return Context.Messages.Where(x => x.FromUserId == userId).ToList();
        }

        public Message GetMessageDetails(Guid id)
        {
            return Context.Messages.Single(x => x.Id == id);
        }

        public void AddMessage(Message message)
        {
            Context.Messages.Add(message);
            Context.SaveChanges();
        }

        public void AddUserMessage(Message message)
        {
            Context.Messages.Add(message);
            Context.SaveChanges();
        }

        public void UpdateMessage(Message message)
        {
            Context.Messages.Update(message);
            Context.SaveChanges();
        }

    }
}
