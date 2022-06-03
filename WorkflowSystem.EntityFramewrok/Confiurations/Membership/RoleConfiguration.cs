using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WorkflowSystem.EntityFramework
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public AppRoleConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            
        }
    }
}
