using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Data;

using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClusteringService _clusteringService;
        private readonly IClassificationService _classificationService;
        private readonly IInvRepository _invRepository;


        public AdminService(IUserRepository userRepository, IClassificationService classificationService, IClusteringService clusteringService, IInvRepository invRepository)
        {
            _userRepository = userRepository;
            _classificationService = classificationService;
            _clusteringService = clusteringService;
            _invRepository = invRepository;
        }

        public void Cluster()
        {
            
            _clusteringService.ClusterAndDetect();


        }

        public object GetUsers()
        {
            return _userRepository.AdminGetUsers().Select(x => new
            {
                x.Id,
                Name = $"{x.LastName} {x.FirstName}",
                x.Username,
                x.UserType,
                x.IsActive,
                x.DateOfBirth,
                x.FirstName,
                x.LastName,
                x.Email
            }).ToList();

        }


        public void BanUser(Guid id)
        {
            User user = _userRepository.GetUserDetails(id);
            user.IsActive = false;
            _userRepository.UpdateUser(user);
        }

        public void UnbanUser(Guid id)
        {
            User user = _userRepository.GetUserDetails(id);
            user.IsActive = true;
            _userRepository.UpdateUser(user);
        }
    }
}
