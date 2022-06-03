using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkflowSystem.Application
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            int maxContent = 5 * 1024 * 1024; //5 MB
            if (value is IFormFile)
            {
                var file = value as IFormFile;
                if (file.Length > maxContent)
                {
                    //ErrorMessage = string.Format(ErrorResource.FileTooLarge, maxContent / (1024 * 1024));
                    ErrorMessage = string.Format("Za du¿y", maxContent / (1024 * 1024));
                    return false;
                }
            }
            else if (value is List<IFormFile>)
            {
                var files = value as List<IFormFile>;
                foreach (var file in files)
                    if (file.Length > maxContent)
                    {
                        ErrorMessage = string.Format("Jeden za du¿y", maxContent / (1024 * 1024));
                        //ErrorMessage = string.Format(ErrorResource.OneFileIsTooLarge, maxContent / (1024 * 1024));
                        return false;
                    }
            }
            return true;
        }
    }  
}
