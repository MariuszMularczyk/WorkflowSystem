using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Data;
using WorkflowSystem.Application.Abstraction;

namespace WorkflowSystem.Application
{
    public interface IInvService : IService
    {
        object GetInvsOnStep(long stepId);
        object GetInvPositions(long id);
        InvDto StartInv(Guid userId);
        InvDto GetInvDetails(long id);
        void MoveInv(InvDto model);
        void CancelInv(InvDto model);
        void DeclineInv(InvDto model);
        void AddInvPosition(InvPositionDto model);
        InvPositionDto GetInvPositionDetails(long id);
        void EditInvPosition(InvPositionDto model);
        void DeleteInvPosition(long id);
        object GetMyTasksList(Guid id);

    }
}
