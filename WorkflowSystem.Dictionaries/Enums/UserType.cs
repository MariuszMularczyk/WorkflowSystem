using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowSystem.Dictionaries
{
    public enum UserType
    {
        [Display(Name = "Użytkownik")]
        User = 1,
        [Display(Name = "Administrator")]
        Admin = 10,
        [Display(Name = "System")]
        System = 999
    }
}
