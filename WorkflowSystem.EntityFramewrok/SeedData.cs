using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;

namespace WorkflowSystem.EntityFramework
{
    public class SeedData
    {
        public static void EnsureSeedData(MainDatabaseContext dbContext)
        {

           
            if (!dbContext.Users.Any())
            {
                 dbContext.Users.AddRangeAsync(
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Password = "$2a$12$X1.ol7hzKMckfmNoD.SypOhAHyMY0YI0bTlmK8d7.xP.uvJ3txpp6",
                    IsActive = true,
                    UserType = UserType.Admin,
                    FirstName = "",
                    LastName = "Administrator",
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "system",
                    Password = "$2a$12$YswdpjKMlfq.uDwMeutIR.14fKKijtcrOE5AfTL8Sgfl/XbhLGzVK",
                    IsActive = true,
                    UserType = UserType.System,
                    FirstName = "",
                    LastName = "System",
                });
               
            }
            dbContext.SaveChanges();

            if (!dbContext.Roles.Any())
            {
               dbContext.Roles.AddRangeAsync(
               new Role()
               {
                   
                   Name = "Kierownicy",
                   Description = "Grupa kierowników firmy",
               },
               new Role()
               {
                   
                   Name = "Managerzy",
                   Description = "Grupa managerów firmy",
               },
               new Role()
               {
                   
                   Name = "Zarząd",
                   Description = "Grupa zarządu firmy",
               });

            }
            dbContext.SaveChanges();
        }
    }
}
