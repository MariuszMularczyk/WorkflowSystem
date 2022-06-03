using WorkflowSystem.Data;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using WorkflowSystem.Infrastructure;
using WorkflowSystem.Resources.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class RoleService : ServiceBase, IRoleService
    {
        #region Dependencies
        public IRoleRepository AppRoleRepository { get; set; }
        public AppRoleConverter AppRoleConverter { get; set; }
        #endregion

        public AppRoleDetailsVM GetAppRoleDetailsVM(int roleId)
        {
            Role role = AppRoleRepository.GetSingle(x => x.Id == roleId);
            AppRoleDetailsVM result = AppRoleConverter.ToAppRoleDetailsVM(role);
            return result;
        }

        public object GetRolesToList()
        {
            return AppRoleRepository.GetRolesToList();
        }

        public int AddRole(AppRoleAddVM model)
        {
            Role role = AppRoleConverter.FromAppRoleAddVM(model);
            AppRoleRepository.Add(role);
            AppRoleRepository.Save();
            return role.Id;
        }

        public void EditRole(AppRoleEditVM model)
        {
            Role role = AppRoleRepository.GetSingle(x => x.Id == model.Id);
            role = AppRoleConverter.FromAppRoleEditVM(model, role);
            AppRoleRepository.Edit(role);
        }

        public AppRoleEditVM GetAppRoleEditVM(int id)
        {
            Role role = AppRoleRepository.GetSingle(x => x.Id == id);
            AppRoleEditVM result = AppRoleConverter.ToAppRoleEditVM(role);
            return result;
        }

        public Role AddRole( string name, string description = "") {
            Role appRole = AppRoleConverter.FromParamsToAppRole(name, description);
            AppRoleRepository.Add(appRole);
            AppRoleRepository.Save();
            return appRole;
        }
    }
}
