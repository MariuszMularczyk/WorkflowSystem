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

namespace WorkflowSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService UserService;

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = UserService.Authenticate(model);

            if (response == null)
                return Ok(new Exception("Nazwa użytkownika lub hasło jest niepoprawne!"));

            return Ok(response);
        }

        [HttpGet("getUsersSelect")]
        public ActionResult<List<SelectModelBinder<Guid>>> GetUsersToSelect()
        {
            return Ok(UserService.GetUsersToSelect());
        }
        [HttpGet("getUsersNotFromGroupSelect")]
        public ActionResult<List<SelectModelBinder<Guid>>> GetUsersNotFromGroupSelect(int roleId)
        {
            return Ok(UserService.GetUsersNotFromGroupSelect(roleId));
        }
    }
}
