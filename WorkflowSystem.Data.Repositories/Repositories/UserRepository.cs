using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.EntityFramework;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Utils;

namespace WorkflowSystem.Data
{
    public class UserRepository : Repository<User, MainDatabaseContext>, IUserRepository
    {

        public UserRepository(MainDatabaseContext context) : base(context)
        { }

        public List<User> GetUsers()
        {
            return Context.Users.Where(x => x.UserType == UserType.User).ToList();
        }
        public object GetUsersByRole(int roleId)
        {
            var userRoles = Context.UserRoles.Where(x => x.User.UserType == UserType.User && x.Role.Id == roleId).Select(x => new
            {
                Id = x.Id,
                Name = x.User.FirstName + " " + x.User.LastName,
            }).ToList();
            return userRoles;
        }

        public User GetUserDetails(Guid id)
        {
            return Context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserDetailsByUsername(string username)
        {
            return Context.Users.SingleOrDefault(x => x.Username == username);
        }

        public bool CheckUsernameUniqueness(string username)
        {
            return !Context.Users.Any(x => x.Username == username);
        }

        public void AddUser(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            Context.Users.Update(user);
            Context.SaveChanges();
        }
        public List<SelectModelBinder<Guid>> GetUsersToSelect()
        {
            List<SelectModelBinder<Guid>> result = _dbset.Where(x => x.UserType == UserType.User).Select(x => new SelectModelBinder<Guid>()
            {
                Value = x.Id,
                Text = x.FirstName + " " + x.LastName,
            }).OrderBy(x => x.Text).ToList();

            return result;
        }
        public List<SelectModelBinder<Guid>> GetUsersNotFromGroupSelect(int roleId)
        {
            List<SelectModelBinder<Guid>> result = _dbset.Where(x => x.UserType == UserType.User && !x.UserRoles.Any(y => y.RoleId == roleId)).Select(x => new SelectModelBinder<Guid>()
            {
                Value = x.Id,
                Text = x.FirstName + " " + x.LastName,
            }).OrderBy(x => x.Text).ToList();

            return result;
        }
        #region Admin

        public List<User> AdminGetUsers()
        {
            return Context.Users.Where(x => x.UserType != UserType.System && x.UserType != UserType.Admin).ToList();
        }

        #endregion

    }
}
