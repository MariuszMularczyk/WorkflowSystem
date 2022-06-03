using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Data;
using WorkflowSystem.Application.Abstraction;

namespace WorkflowSystem.Application
{
    public interface IClusteringService : IService
    {

        void ClusterAndDetect();

    }
}
