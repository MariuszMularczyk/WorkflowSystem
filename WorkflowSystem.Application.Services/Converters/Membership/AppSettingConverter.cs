using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class AppSettingConverter : ConverterBase
    {
        
        public AppSetting FromAppSettingAddVM(AppSettingAddVM model)
        {
            AppSetting setting = new AppSetting
            {
                Value = model.Value,
                Type = model.Type,
            };
            return setting;
        }

      
    }
}
