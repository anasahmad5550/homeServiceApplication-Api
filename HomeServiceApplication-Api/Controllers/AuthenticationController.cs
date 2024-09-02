using Azure;
using HomeServiceApplication_Api.Controllers.Shared;
using HomeServiceApplication_Api.Filters;
using HomeServiceApplication_Api.ViewModels;
using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.Common.Utilities;
using HSA.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeServiceApplication_Api.Controllers
{
    [ServiceFilter(typeof(LoggerAttribute))]
    public class AuthenticationController : BaseApiController
    {
        private readonly AuthenticationService _service;
        private readonly ILogger<object> _logger;
        private readonly AppConfig _config;

        public AuthenticationController(IOptions<AppConfig> options, ILoggerFactory loggerFactory)
        {
            _config = options.Value;
            _logger = loggerFactory.CreateLogger<object>();
            _service = new AuthenticationService(_config, _logger);
        }

        #region POST
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<ApiResponse<string>> Login(LoginVM vM)
        {
            string msg = "";
            if (_service.AuthenticateUser(vM.ToServiceModel(vM), out msg)) {
               string token =  _service.getToken(vM.ToServiceModel(vM));
                return Ok(token);
            }
            return BadRequest(msg);
        }
        #endregion
    }
}
