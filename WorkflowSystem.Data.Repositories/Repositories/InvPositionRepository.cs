using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.EntityFramework;

namespace WorkflowSystem.Data
{
    public class InvPositionRepository : Repository<InvPosition, MainDatabaseContext>, IInvPositionRepository
    {
        public InvPositionRepository(MainDatabaseContext context) : base(context)
        { }


        public List<InvPosition> GetInvPositions(long id)
        {
            return _dbset.Where(x => x.InvId == id).ToList();
        }
        public InvPosition GetInvPositionDetails(long id)
        {
            return _dbset.FirstOrDefault(x => x.Id == id);
        }

    }
}
