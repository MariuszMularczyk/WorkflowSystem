using WorkflowSystem.Dictionaries;
using WorkflowSystem.Resources.Shared;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class OrderAddVM
    {
        public string Table { get; set; }
        public List<ProductOrderAddVM> ProductOrders { get; set; }
    }
}

