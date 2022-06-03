using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface IApplicationFileRepository : IRepository<ApplicationFile>
    {
        ApplicationFile GetImage(int id);
    }
}
