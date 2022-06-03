using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Application;
using WorkflowSystem.Data;
using WorkflowSystem.Domain;

namespace WorkflowSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private IAdminService AdminService;

        public AdminsController(IAdminService service)
        {
            AdminService = service;
        }

        [HttpGet("getUsersList")]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(AdminService.GetUsers());
        }

        [HttpPost("banUser")]
        public ActionResult BanUser(GuidDto model)
        {
            AdminService.BanUser(model.Id);
            return Ok();
        }

        [HttpPost("unbanUser")]
        public ActionResult UnbanUser(GuidDto model)
        {
            AdminService.UnbanUser(model.Id);
            return Ok();
        }

        [HttpGet("cluster")]
        public ActionResult<IEnumerable<User>> Cluster()
        {
            AdminService.Cluster();
            return Ok();
        }
    }
}
