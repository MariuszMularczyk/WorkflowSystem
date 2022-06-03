using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowSystem.Dictionaries
{
    public enum MessageType
    {
        [Display(Name = "Prywatna")]
        Private = 0,
        [Display(Name = "Systemowa")]
        System = 3,
    }
}
