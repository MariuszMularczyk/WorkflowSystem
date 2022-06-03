using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WorkflowSystem.Domain;
using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Data;
using WorkflowSystem.Helpers;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Utils;

namespace WorkflowSystem.Application
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            User user = _userRepository.GetUserDetailsByUsername(model.Username);
            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(model.Password, user.Password) ||
                user.UserType == UserType.System ||
                !user.IsActive)
            {
                return null;
            }
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public User GetById(Guid id)
        {
            return _userRepository.GetUserDetails(id);
        }

        public Guid AddUserAccountForNewPlayer(RegisterUserDto playerModel)
        {
            User user = new User()
            {
                Username = playerModel.LoginUsername,
                Password = BCrypt.Net.BCrypt.HashPassword(playerModel.LoginPassword),
                Email = playerModel.Email,
                IsActive = true
            };
            _userRepository.AddUser(user);

            return user.Id;
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public List<SelectList> GetUsersToLookup()
        {
            List<SelectList> users = _userRepository.GetUsers().Select(x => new SelectList
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }).ToList();

            return users;
        }


        public object GetUsers()
        {
            return _userRepository.GetUsers();
        }
        public object GetUsersByRole(int roleId)
        {
            return _userRepository.GetUsersByRole(roleId);
        }
        public GetUserDataDto GetUserDataDetails(Guid id)
        {
            User userData = _userRepository.GetUserDetails(id);
            if (userData == null)
            {
                throw new KeyNotFoundException();
            }

            GetUserDataDto model = new GetUserDataDto()
            {
                Id = userData.Id,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                DateOfBirth = userData.DateOfBirth,
            };

            if (userData.Email != null)
            {
                model.Email = userData.Email;
            }

            return model;
        }


        public void AddUser(RegisterUserDto playerModel)
        {
            User user = new User()
            {
                Username = playerModel.LoginUsername,
                Password = BCrypt.Net.BCrypt.HashPassword(playerModel.LoginPassword),
                Email = playerModel.Email,
                IsActive = false,
                FirstName = playerModel.FirstName,
                LastName = playerModel.LastName,
                DateOfBirth = playerModel.DateOfBirth.AddHours(12),
                UserType = UserType.User
            };

            _userRepository.AddUser(user);
        }

        public void EditUserData(EditUserDto model)
        {
            User userData = _userRepository.GetUserDetails(model.Id);
            userData.FirstName = model.FirstName;
            userData.LastName = model.LastName;
            userData.DateOfBirth = model.DateOfBirth;
            userData.Email = model.Email;

            _userRepository.UpdateUser(userData);
        }

        public bool CheckUsernameUniqueness(string username)
        {
            return _userRepository.CheckUsernameUniqueness(username);
        }

        public List<SelectModelBinder<Guid>> GetUsersToSelect()
        {
            return _userRepository.GetUsersToSelect();
        }
        public List<SelectModelBinder<Guid>> GetUsersNotFromGroupSelect(int roleId)
        {
            return _userRepository.GetUsersNotFromGroupSelect(roleId);
        }
        
    }
}
