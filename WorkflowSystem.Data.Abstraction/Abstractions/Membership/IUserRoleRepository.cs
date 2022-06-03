using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        object GetUserRoles(Guid userId);
        object GetRoleUsers(int roleId);
    }
}
