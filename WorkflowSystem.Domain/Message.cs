
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Domain
{

    public class Message 
    {
        public Message()
        {
        }

        public Guid Id { get; set; }
        public Guid? FromUserId { get; set; }
        public Guid? ToUserId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime SentOn { get; set; }
        public MessageType MessageType { get; set; }
        public bool IsActive { get; set; }
        public bool IsRead { get; set; }
        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}
