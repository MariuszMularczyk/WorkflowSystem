using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Data
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid? FromUserId { get; set; }
        public Guid? ToUserId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime SentOn { get; set; }
        public MessageType MessageType { get; set; }
        public bool IsActive { get; set; }
        public bool IsRead { get; set; }
        public  GetUserDataDto FromUser { get; set; }
        public  GetUserDataDto ToUser { get; set; }

    }
}
