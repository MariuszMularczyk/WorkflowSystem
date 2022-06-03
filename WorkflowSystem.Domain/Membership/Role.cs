using WorkflowSystem.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Domain
{
    public class Role 
    {
        public Role()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
