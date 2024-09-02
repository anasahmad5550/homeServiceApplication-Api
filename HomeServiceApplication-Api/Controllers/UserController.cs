using HomeServiceApplication_Api.Controllers.Shared;
using HomeServiceApplication_Api.Filters;
using HomeServiceApplication_Api.ViewModels;
using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.Common.Utilities;
using HSA.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace HomeServiceApplication_Api.Controllers
{
    [ServiceFilter(typeof(LoggerAttribute))]
    public class UserController : BaseApiController
    {
        private readonly UserService _service;
        private readonly ILogger<object> _logger;
        private readonly AppConfig _config;
        public UserController(IOptions<AppConfig> options, ILoggerFactory loggerFactory)
        {
            _config = options.Value;
            _logger = loggerFactory.CreateLogger<object>();
            _service = new UserService(_config, _logger);
        }
        #region POST
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public ActionResult<ApiResponse<int>> SignUp(UserVM vm)
        {
            ApiResponse<int> response = new();
            int code;
            string message;

            try
            {
                int UserId = _service.CreateUser(vm.ToServiceModel(vm), out code, out message);
                if (UserId > 0)
                {
                    return Ok(response.GetSuccessResponseObject(UserId, message));
                }
                else
                {
                    return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, message));
                }
            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }
        #endregion
    }
}
