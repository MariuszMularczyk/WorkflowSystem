using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowSystem.Application.Abstraction;

namespace WorkflowSystem.Application
{
    public interface IRoleService : IService
    {
        object GetRolesToList();
        AppRoleDetailsVM GetAppRoleDetailsVM(int roleId);
        int AddRole(AppRoleAddVM model);
        void EditRole(AppRoleEditVM model);
        AppRoleEditVM GetAppRoleEditVM(int id);
        Role AddRole(string name, string description = "");
    }
}
