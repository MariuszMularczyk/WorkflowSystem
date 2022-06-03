using WorkflowSystem.Dictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkflowSystem.Application
{
    public class AppSettingAddVM
    {
        public AppSettingEnum Type { get; set; }
        public string Value { get; set; }
    }
}