using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Utils;

namespace WorkflowSystem.Data
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetUsers();
        object GetUsersByRole(int roleId);
        User GetUserDetails(Guid id);
        User GetUserDetailsByUsername(string username);
        bool CheckUsernameUniqueness(string username);
        void AddUser(User user);
        void UpdateUser(User user);
        List<User> AdminGetUsers();
        public List<SelectModelBinder<Guid>> GetUsersToSelect();
        public List<SelectModelBinder<Guid>> GetUsersNotFromGroupSelect(int roleId);
        
    }
}
