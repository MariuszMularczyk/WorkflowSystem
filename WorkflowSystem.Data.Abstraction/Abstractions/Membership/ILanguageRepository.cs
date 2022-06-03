using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Language GetLanguage(LanguageDictionary languageDictionary);
        List<SelectModelBinder<int>> GetLanguagesToSelect();
    }
}
