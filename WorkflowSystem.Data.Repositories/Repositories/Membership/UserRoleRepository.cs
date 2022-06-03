using WorkflowSystem.Domain;
using WorkflowSystem.EntityFramework;
using WorkflowSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Data
{
    public class UserRoleRepository : Repository<UserRole, MainDatabaseContext>, IUserRoleRepository
    {
        public UserRoleRepository(MainDatabaseContext context)
         : base(context)
        {
        }


        public object GetUserRoles(Guid userId)
        {
            List<UserRoleListDTO> result = _dbset.Where(x => x.UserId == userId).Select(x => new UserRoleListDTO()
            {
                UserRoleId = x.Id,
                RoleName = x.Role.Name
            }).ToList();
            
            return result;
        }
        public object GetRoleUsers( int roleId)
        {
            List<RoleUserListDTO> result = _dbset.Where(x => x.RoleId == roleId).Select(x => new RoleUserListDTO()
            {
                UserRoleId = x.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                FullName = x.User.LastName + " " + x.User.FirstName
            }).ToList();
            return result;
        }
    }
}
