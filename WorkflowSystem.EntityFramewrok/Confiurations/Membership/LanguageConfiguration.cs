using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.EntityFramework
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public LanguageConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<Language> builder)
        {


        }
    }
}
