using WorkflowSystem.Domain;
using WorkflowSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Utils;

namespace WorkflowSystem.Application
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(Guid id);
        Guid AddUserAccountForNewPlayer(RegisterUserDto playerModel);
        List<SelectList> GetUsersToLookup();

        object GetUsers();
        object GetUsersByRole(int roleId);
        GetUserDataDto GetUserDataDetails(Guid id);
        void AddUser(RegisterUserDto playerModel);
        void EditUserData(EditUserDto model);
        bool CheckUsernameUniqueness(string username);
        List<SelectModelBinder<Guid>> GetUsersToSelect();
        List<SelectModelBinder<Guid>> GetUsersNotFromGroupSelect(int roleId);
    }
}
