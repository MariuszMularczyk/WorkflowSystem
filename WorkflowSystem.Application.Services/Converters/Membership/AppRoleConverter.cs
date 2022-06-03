using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class AppRoleConverter : ConverterBase
    {
        public AppRoleDetailsVM ToAppRoleDetailsVM(Role role)
        {
            var result = new AppRoleDetailsVM()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };

            return result;
        }
        public AppRoleEditVM ToAppRoleEditVM(Role role)
        {
            var result = new AppRoleEditVM()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return result;
        }
        public Role FromAppRoleEditVM(AppRoleEditVM model, Role role)
        {
            role.Name = model.Name;
            role.Description = model.Description;
            return role;
        }
        public Role FromAppRoleAddVM(AppRoleAddVM model)
        {
            Role role = new Role
            {
                Name = model.Name,
                Description = model.Description,
            };
            return role;
        }

        public Role FromParamsToAppRole( string name, string description)
        {
            Role appRole = new Role()
            {
                Name = name,
                Description = description,
            };
            return appRole;
        }
    }
}
