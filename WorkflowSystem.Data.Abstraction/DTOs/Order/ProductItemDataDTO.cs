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
    public class ProductItemDataDTO
    {
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public int? Priority { get; set; }
        public decimal? Measure { get; set; }
        public int OrderId { get; set; }
        public string Table { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public decimal TimeOfPreparation { get; set; }

    }
}

