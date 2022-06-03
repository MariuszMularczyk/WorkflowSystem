using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.EntityFramework;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Data
{
    public class InvRepository : Repository<Inv, MainDatabaseContext>, IInvRepository
    {
        public InvRepository(MainDatabaseContext context) : base(context)
        { }


        public List<Inv> GetInvsOnStep(long id)
        {
            return _dbset.Where(x => x.Step == (StepEnum)id).ToList();
        }

        public Inv GetInvDetails(long id)
        {
            return _dbset.Single(x => x.Id == id);
        }

    }
}
