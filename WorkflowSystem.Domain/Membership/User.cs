using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Domain
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public virtual List<Message> SentMessages { get; set; }
        public virtual List<Message> ReceivedMessages { get; set; }
        public virtual List<Inv> RegistredInvs { get; set; }
        public virtual List<Inv> VerificatedInvs { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
