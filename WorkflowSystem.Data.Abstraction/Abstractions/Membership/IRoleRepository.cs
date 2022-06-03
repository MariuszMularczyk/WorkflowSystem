using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface IRoleRepository : IRepository<Role>
    {
        object GetRolesToList();
        List<SelectModelBinder<int>> GetRolesToSelect();
    }
}
