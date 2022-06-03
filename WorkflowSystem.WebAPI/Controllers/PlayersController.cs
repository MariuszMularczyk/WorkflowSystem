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
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;

namespace WorkflowSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IUserService UserService;

        public PlayersController(IUserService service)
        {
            UserService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetPlayers(Guid? teamId)
        {
            return Ok(UserService.GetUsers());
        }


        [HttpGet("getUsersByRole")]
        public ActionResult<object> GetPlayers(int roleId)
        {
            return Ok(UserService.GetUsersByRole(roleId));
        }


        [HttpGet("details")]
        public ActionResult<GetUserDataDto> GetUsers(Guid id)
        {
            return Ok(UserService.GetUserDataDetails(id));
        }

        [HttpGet("userDataDetails")]
        public ActionResult<GetUserDataDto> GetUserDataDetails(Guid id)
        {
            return Ok(UserService.GetUserDataDetails(id));
        }

        [HttpPost]
        public ActionResult PostPlayer([FromBody] RegisterUserDto playerModel)
        {
            UserService.AddUser(playerModel);

            return Ok();
        }

        [HttpPost("edit")]
        public ActionResult EditPlayer([FromBody] EditUserDto playerModel)
        {
            UserService.EditUserData(playerModel);

            return Ok();
        }

        [HttpGet("isUsernameUnique")]
        public bool IsUsernameUnique(string username)
        {
            bool result = UserService.CheckUsernameUniqueness(username);
            return result;
        }

        [HttpGet("getPlayerByUserId")]
        public ActionResult<GetUserDataDto> GetPlayerByUserId(Guid userId)
        {
            return Ok(UserService.GetUserDataDetails(userId));
        }

        [HttpGet("getUserDataByUserId")]
        public ActionResult<GetUserDataDto> GetUserDataByUserId(Guid userId)
        {
            return Ok(UserService.GetUserDataDetails(userId));
        }

        [HttpGet("getUserTypesToLookup")]
        public ActionResult GetPenaltyTypesToLookup()
        {
            return Ok(EnumExtensions.GetValues<UserType>());
        }

    }
}
