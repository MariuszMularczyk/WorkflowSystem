using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class UserRoleConverter : ConverterBase
    {
        public UserRole FromAppUserEditVM(UserRole appUserRole, Role appRole)
        {
            appUserRole.RoleId = appRole.Id;
            return appUserRole;
        }
    }
}
