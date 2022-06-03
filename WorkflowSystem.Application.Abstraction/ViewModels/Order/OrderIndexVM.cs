using WorkflowSystem.Dictionaries;
using WorkflowSystem.Resources.Shared;
using WorkflowSystem.Utils;
using WorkflowSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class OrderIndexVM
    {
        public OrderIndexVM()
        {
        }

        public string OrderStatuses { get; set; }
    }
}

