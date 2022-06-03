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
    public class OrderListDTO
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string Table { get; set; }
        public string ProductTypeName { get; set; }
        public DateTime TimeOfOrder { get; set; }
        public DateTime? TimeOfSugestedPrepare { get; set; }
        public decimal? TimeOfPreparation { get; set; }
        public List<decimal> TimeOfPreparations { get; set; }
        public string Order { get; set; }
        public ProductType? ProductType { get; set; }
        public List<ProductItemDTO> ProductList { get; set; }
}
}

