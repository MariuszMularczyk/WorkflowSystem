using WorkflowSystem.Domain;
using WorkflowSystem.EntityFramework;
using WorkflowSystem.Infrastructure;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public class RoleRepository : Repository<Role, MainDatabaseContext>, IRoleRepository
    {
        public RoleRepository(MainDatabaseContext context)
         : base(context)
        {
        }
        public object GetRolesToList()
        {
            List<RolerListDTO> result = _dbset.Select(x => new RolerListDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
           
            return result;
        }

        public List<SelectModelBinder<int>> GetRolesToSelect()
        {
            List<SelectModelBinder<int>> result = _dbset.Select(x => new SelectModelBinder<int>()
            {
                Value = x.Id,
                Text = x.Name
            }).OrderBy(x => x.Text).ToList();

            return result;
        }
    }
}
