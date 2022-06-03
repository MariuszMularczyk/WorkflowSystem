using WorkflowSystem.Data;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using WorkflowSystem.Infrastructure;
using WorkflowSystem.Resources.Shared;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class UserRoleService : ServiceBase, IUserRoleService
    {
        #region Dependencies

        public IUserRoleRepository AppUserRoleRepository { get; set; }
        public IUserRepository AppUserRepository { get; set; }
        public IRoleRepository AppRoleRepository { get; set; }
        #endregion

        public void Delete(long userRoleIdId)
        {
            AppUserRoleRepository.DeleteWhere(x => x.Id == userRoleIdId);
            AppUserRoleRepository.Save();
        }
        public void AddUserToRole(int roleId, Guid userId)
        {
            AppUserRoleRepository.Add(new UserRole() { RoleId = roleId, UserId = userId});
            AppUserRoleRepository.Save();
        }
        public object GetUserRoles(Guid userId)
        {
            return AppUserRoleRepository.GetUserRoles(userId);
        }
        public object GetRoleUsers(int roleId)
        {
            return AppUserRoleRepository.GetRoleUsers(roleId);
        }
        public bool IsUserInRole(Guid userId, int roleId)
        {
            return AppUserRoleRepository.Any(x => x.UserId == userId && x.RoleId == roleId);
        }
        public AppUserRoleAddToRoleVM GetAppUserRoleAddToRoleVM(int roleId)
        {
            Role role = AppRoleRepository.GetSingle(x => x.Id == roleId);
            if (role == null)
                throw new BussinesException(2, ErrorResource.RoleNotFound);
            AppUserRoleAddToRoleVM result = new AppUserRoleAddToRoleVM()
            {
                RoleId = role.Id,
                RoleName = role.Name,
            };
            return result;
        }

        public AppUserRoleAddToUserVM GetAppUserRoleAddToUserVM(Guid userId, AppUserRoleAddToUserVM model = null)
        {
            if (model == null)
            {
                model = new AppUserRoleAddToUserVM();
                User user = AppUserRepository.GetSingle(x => x.Id == userId);
                if (user == null)
                    throw new BussinesException(2, ErrorResource.UserNotFound);

                model.UserId = user.Id;
                model.UserName = user.LastName + " " + user.FirstName;
            }
            model.Roles = AppRoleRepository.GetRolesToSelect();

            return model;
        }

        public void AddUserToRole(Guid userId, int roleId)
        {
            if (IsUserInRole(userId, roleId))
                return;
            UserRole crmUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            AppUserRoleRepository.Add(crmUserRole);
        }
    }
}
