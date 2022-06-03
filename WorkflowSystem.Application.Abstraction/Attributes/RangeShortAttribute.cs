using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkflowSystem.Application
{
    public class RangeShortAttribute : RangeAttribute, IClientModelValidator
    {
        private RangeEnum _range;
        public RangeShortAttribute(double min, double max)
            : base(min, max)
        {
            _range = RangeEnum.Between;
            base.ErrorMessage = GetMessage();
        }
        public RangeShortAttribute(int min = int.MinValue, int max = int.MaxValue, RangeEnum range = RangeEnum.Between)
          : base(min, max)
        {
            _range = range;
            base.ErrorMessage = GetMessage();
        }



        private string GetMessage()
        {
            switch (_range)
            {
                case RangeEnum.Between:
                    return "Pomiêdzy";
                case RangeEnum.MinOnly:
                    return "Minimum";
                case RangeEnum.MaxOnly:
                    return "Maksimum";
                default:
                    return "Po za zakresem";
            }
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            //MergeAttribute(context.Attributes, "data-val-required", FormatErrorMessage(""));
        }

        bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
