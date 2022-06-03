using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Data;

namespace WorkflowSystem.Application
{
    public interface IAdminService
    {
        object GetUsers();
        void BanUser(Guid id);
        void UnbanUser(Guid id);
        void Cluster();
    }
}
