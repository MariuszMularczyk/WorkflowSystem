using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Data;
using WorkflowSystem.Application.Abstraction;

namespace WorkflowSystem.Application
{
    public interface IClassificationService : IService
    {
        void Train(IEnumerable<InvData> data);
        int Predict(InvData position);
        string Test();
    }
}
