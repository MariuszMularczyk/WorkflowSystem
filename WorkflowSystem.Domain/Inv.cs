
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Domain
{

    public class Inv 
    {
        public Inv()
        {
        }

        public long Id { get; set; }
        public string ClientName { get; set; }
        public string NIP { get; set; }
        public DateTime? IntroductionDate { get; set; }
        public string FvNumber { get; set; }
        public Guid? IntroducerId { get; set; }
        public virtual User Introducer { get; set; }
        public Guid? MeritId { get; set; }
        public virtual User Merit { get; set; }

        public DateTime? DateOfReceipt { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public DateTime? SaleDate { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public decimal? NetValue { get; set; }
        public int? PaymentType { get; set; }
        public int? FVType { get; set; }
        public decimal? GrossValue { get; set; }
        public int? Currency { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? VAT { get; set; }
        public StepEnum Step { get; set; }

        public virtual List<InvPosition> Positions { get; set; }
    }
}
