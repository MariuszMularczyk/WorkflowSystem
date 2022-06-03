using WorkflowSystem.Dictionaries;
using WorkflowSystem.Utils;
using WorkflowSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public class OrderItemDTO
    {
        public OrderStatusEnum OrderStatus { get; set; }
        public int OrderId { get; set; }
        public string Table { get; set; }
        public DateTime TimeOfOrder { get; set; }

    }
}

