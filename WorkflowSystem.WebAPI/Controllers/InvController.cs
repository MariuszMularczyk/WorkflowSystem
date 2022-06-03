using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using WorkflowSystem.Application;
using WorkflowSystem.Utils;
using WorkflowSystem.Data;

namespace WorkflowSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvController : ControllerBase
    {
        private IInvService InvService;
        private IUserRoleService UserRoleService;
        
        public InvController(IInvService invService, IUserRoleService userRoleService)
        {
            InvService = invService;
            UserRoleService = userRoleService;
        }

        [HttpPost("isUserInRole")]
        public bool IsUserInRole(IntGuidDto userId)
        {

            return UserRoleService.IsUserInRole(userId.Id, userId.IntId);
        }

        [HttpGet("startInv")]
        public InvDto StartInv(Guid userId)
        {

            return InvService.StartInv(userId);
        }

        [HttpPost("moveInv")]
        public ActionResult MoveInv(InvDto inv)
        {
            InvService.MoveInv(inv);
            return Ok();
        }

        [HttpPost("declineInv")]
        public ActionResult DeclineInv(InvDto inv)
        {
            InvService.DeclineInv(inv);
            return Ok();
        }

        [HttpPost("cancelInv")]
        public ActionResult CancelInv(InvDto inv)
        {
            InvService.CancelInv(inv);
            return Ok();
        }

        [HttpGet("getInvDetails")]
        public InvDto GetInvDetails(int id)
        {

            return InvService.GetInvDetails(id);
        }

        [HttpGet("getInvPositions")]
        public object GetInvPositions(long id)
        {

            return InvService.GetInvPositions(id);
        }

        [HttpGet("getUsersByRole")]
        public ActionResult<object> GetPlayers(long roleId)
        {
            return Ok(InvService.GetInvPositionDetails(roleId));
        }

        [HttpPost("addInvPosition")]
        public void AddInvPosition(InvPositionDto invPositionDto)
        {
            InvService.AddInvPosition(invPositionDto);
        }

        [HttpGet("getInvPositionDetails")]
        public InvPositionDto GetInvPositionDetails(int id)
        {

            return InvService.GetInvPositionDetails(id);
        }
        [HttpPost("updateInvPosition")]
        public void UpdateInvPosition(InvPositionDto invPositionDto)
        {
            InvService.EditInvPosition(invPositionDto);
        }
        [HttpPost("deleteInvPosition")]
        public void DeleteInvPosition(IntDto dto)
        {
            InvService.DeleteInvPosition(dto.Id);
        }


        [HttpGet("getMyTasksList")]
        public object GetMyTasksList(Guid id)
        {

            return InvService.GetMyTasksList(id);
        }

        [HttpGet("getVerificationList")]
        public object GetVerificationList()
        {

            return InvService.GetInvsOnStep(2);
        }

        [HttpGet("getSupervisorAcceptanceList")]
        public object GetSupervisorAcceptanceList()
        {

            return InvService.GetInvsOnStep(3);
        }

        [HttpGet("getManagerAcceptanceList")]
        public object GetManagerAcceptanceList()
        {

            return InvService.GetInvsOnStep(4);
        }

        [HttpGet("getManagementAcceptanceList")]
        public object GetManagementAcceptanceList()
        {

            return InvService.GetInvsOnStep(5);
        }

        [HttpGet("getAcceptedList")]
        public object GetAcceptedList()
        {

            return InvService.GetInvsOnStep(6);
        }

        [HttpGet("getRecivedList")]
        public object GetRecivedList()
        {

            return InvService.GetInvsOnStep(7);
        }

        [HttpGet("getRejectedList")]
        public object GetRejectedList()
        {

            return InvService.GetInvsOnStep(10);
        }

        [HttpGet("getClosedList")]
        public object GetClosedList()
        {

            return InvService.GetInvsOnStep(99);
        }

    }
}
