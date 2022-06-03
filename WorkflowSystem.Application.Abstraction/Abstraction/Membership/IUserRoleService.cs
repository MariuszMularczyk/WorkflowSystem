using WorkflowSystem.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowSystem.Application.Abstraction;


namespace WorkflowSystem.Application
{
    public interface IUserRoleService : IService
    {
        object GetUserRoles(Guid userId);
        void AddUserToRole(int roleId, Guid userId);
        void Delete(long userRoleId);
        object GetRoleUsers(int roleId);
        AppUserRoleAddToRoleVM GetAppUserRoleAddToRoleVM(int roleId);
        bool IsUserInRole(Guid userId, int roleId);
        void AddUserToRole(Guid userId, int roleId);
        AppUserRoleAddToUserVM GetAppUserRoleAddToUserVM(Guid userId, AppUserRoleAddToUserVM model = null);
    }
}
