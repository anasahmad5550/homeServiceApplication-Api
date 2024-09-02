using HSA.Common.Utilities;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;
using HSA.Services.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HSA.Services.Services
{
    public class UserService : BaseService<UserSM, User>
    {
        private readonly AppConfig _appConfig;
        private ILogger _logger;

        public UserService(AppConfig appConfig, ILogger logger) : base(appConfig) 
        {
            _logger = logger;
            _appConfig = appConfig;
        }

        public int CreateUser(UserSM userSm, out int code, out string message)
        {
            try
            {
                var salt = PasswordHelper.GenerateSalt();
                userSm.PasswordHash = PasswordHelper.HashPassword(userSm.PasswordHash, salt);
                userSm.salt = salt;
                Insert(userSm);
                uow.SaveChanges();
                int UserId = Get().Id;
                if (UserId > 0)
                {
                    _logger.LogInformation($"CustomLog:CreateUser:User Created, UserID: {userSm.Id}");
                    code = (int)HttpStatusCode.OK;
                    message = "User Created Successfully";
                    return UserId;
                }
                else
                {
                    _logger.LogInformation($"CustomLog:CreateUser:Failed to create user");
                    code = (int)HttpStatusCode.BadRequest;
                    message = "Faild to create User";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:AuthenticationService: Error Occured while Authenticate User. Exp: {ex}");
                message = $"Faild to create User {ex.Message}";
                code = (int)HttpStatusCode.BadRequest;
                return -1;
            }
            

        }
    }
}
