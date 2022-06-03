using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Domain
{
    [Table("Orders")]
    public class Order : Entity
    {
        public string Table { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }
        public DateTime TimeOfOrder { get; set; }

    }
}
