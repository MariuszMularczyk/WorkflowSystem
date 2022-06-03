using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface IInvPositionRepository : IRepository<InvPosition>
    {
        List<InvPosition> GetInvPositions(long id);
        InvPosition GetInvPositionDetails(long id);
    }
}
