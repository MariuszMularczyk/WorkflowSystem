using WorkflowSystem.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class AppRoleAddVM
    {

        public AppRoleAddVM()
        {

        }
        [RequiredShort]
        [Display(ResourceType = typeof(SharedResource), Name = "Name")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "Description")]
        public string Description { get; set; }

    }
}
