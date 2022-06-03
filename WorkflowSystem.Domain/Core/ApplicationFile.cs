using WorkflowSystem.Dictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Domain
{
    [Table("ApplicationFiles")]
    public class ApplicationFile 
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}

