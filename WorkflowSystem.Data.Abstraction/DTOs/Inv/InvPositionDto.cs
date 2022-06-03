using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Data
{
    public class InvPositionDto
    {
        public long? Id { get; set; }
        public string Descryption { get; set; }
        public decimal Quantity { get; set; }
        public int JM { get; set; }
        public decimal NetValueUnit { get; set; }
        public decimal NetValue { get; set; }
        public long VAT { get; set; }
        public decimal GrossValue { get; set; }
        public long InvId { get; set; }
        public bool OverPaid { get; set; }
    }
}
