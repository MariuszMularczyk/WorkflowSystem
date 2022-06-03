using WorkflowSystem.Domain;
using WorkflowSystem.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public class ApplicationFileRepository : Repository<ApplicationFile, MainDatabaseContext>, IApplicationFileRepository
    {
        public ApplicationFileRepository(MainDatabaseContext context) : base(context)
        { }

        public ApplicationFile GetImage(int id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
