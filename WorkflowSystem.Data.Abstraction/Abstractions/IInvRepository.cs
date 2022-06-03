using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface IInvRepository : IRepository<Inv>
    {
        List<Inv> GetInvsOnStep(long id);
        Inv GetInvDetails(long id);
    }
}
