using HomeServiceApplication_Api.Controllers.Shared;
using HomeServiceApplication_Api.Filters;
using HomeServiceApplication_Api.ViewModels.Shared;
using HomeServiceApplication_Api.ViewModels;
using HSA.Common.Utilities;
using HSA.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using HSA.Common.Models;
using System.Net;
using System.Security.Claims;

namespace HomeServiceApplication_Api.Controllers
{
    [ServiceFilter(typeof(LoggerAttribute))]
    public class ServiceController : BaseApiController
    {
        private readonly CompanyService _service;
        private readonly ILogger<object> _logger;
        private readonly AppConfig _config;
        public ServiceController(IOptions<AppConfig> options, ILoggerFactory loggerFactory)
        {
            _config = options.Value;
            _logger = loggerFactory.CreateLogger<object>();
            _service = new CompanyService(_config, _logger);

        }
        #region GET
        [HttpGet]
        public ActionResult<ApiGridResponse<ServiceVM>> Index([FromQuery] SearchRequestModel vm)
        {
            var response = new ApiGridResponse<ServiceVM>();
            try
            {
                _logger.LogInformation($"Going to fetch Clients");
                int totalCount;
                var serviceList = _service.GetAllServices(vm, out totalCount);
                var result = new ServiceVM().FromServiceModelList(serviceList).ToList();

                if (serviceList != null && serviceList.Any())
                {
                    var resp = response.GetSuccessResponseObject(result, Constant.GET_API_SUCCESS_MSG);
                    resp.totalCount = totalCount;
                    return Ok(resp);
                }
                else
                {
                    return Ok(response.GetNullResponseObject());
                }

            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ServiceVM>> Get(int id)
        {
            var response = new ApiResponse<ServiceVM>();
            try
            {
                _logger.LogInformation($"Going to fetch Service");
                var service = _service.GetService(id);

                if (service != null)
                {
                    var result = new ServiceVM().FromServiceModel(service);
                    var resp = response.GetSuccessResponseObject(result, Constant.GET_API_SUCCESS_MSG);
                    return Ok(resp);
                }
                else
                {
                    return Ok(response.GetNullResponseObject());
                }

            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }

        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<ApiResponse<int>> Post(ServiceVM vm)
        {
            ApiResponse<int> response = new();
            int code;
            string message;

            try
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int serviceId = _service.CreateService(vm.ToServiceModel(vm), out code, out message, UserId);
                if (serviceId > 0)
                {
                    return Ok(response.GetSuccessResponseObject(serviceId, message));
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
