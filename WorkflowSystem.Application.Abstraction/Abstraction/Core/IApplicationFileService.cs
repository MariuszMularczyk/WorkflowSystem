using WorkflowSystem.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public interface IApplicationFileService : IService
    {
        ImageVM GetImage(int id);
    }
}
