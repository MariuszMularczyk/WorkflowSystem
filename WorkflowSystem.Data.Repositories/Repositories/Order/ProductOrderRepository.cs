using WorkflowSystem.Domain;
using WorkflowSystem.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public class ProductOrderRepository : Repository<ProductOrder, MainDatabaseContext>, IProductOrderRepository
    {
        public ProductOrderRepository(MainDatabaseContext context) : base(context)
        { }

    }
}
