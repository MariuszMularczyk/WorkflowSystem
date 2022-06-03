using WorkflowSystem.Resources.Shared;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class AppUserRoleAddToUserVM
    {
        public AppUserRoleAddToUserVM()
        {
            Roles = new List<SelectModelBinder<int>>();
        }
        public Guid UserId { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "Person")]
        public string UserName { get; set; }

        [RequiredShort]
        [RemoteApp("IsNotUserInRole", "AppUserRoles", "UserId", ErrorMessageResourceType = typeof(ErrorResource), ErrorMessageResourceName = "UserAddedToRole")]
        [Display(ResourceType = typeof(SharedResource), Name = "Role")]
        public int? RoleId { get; set; }

        public List<SelectModelBinder<int>> Roles { get; set; }
    }
}
