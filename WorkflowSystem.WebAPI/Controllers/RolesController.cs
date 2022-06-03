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
    public class RolesController : ControllerBase
    {
        private IRoleService RoleService;
        private IUserRoleService UserRoleService;
        public RolesController(IRoleService roleService, IUserRoleService userRoleService)
        {
            RoleService = roleService;
            UserRoleService = userRoleService;
        }

        [HttpGet("getRoles")]
        public ActionResult<object> GetRoles()
        {
            return Ok(RoleService.GetRolesToList());
        }

        [HttpPost("deleteRole")]
        public ActionResult DeleteRole(IntDto dto)
        {
            UserRoleService.Delete(dto.Id);
            return Ok();
        }
        [HttpPost("addUserToRole")]
        public ActionResult AddUserToRole(IntGuidDto dto)
        {
            UserRoleService.AddUserToRole(dto.IntId, dto.Id);
            return Ok();
        }
    }
}
