using HomeServiceApplication_Api.Controllers.Shared;
using HomeServiceApplication_Api.ViewModels.Shared;
using HomeServiceApplication_Api.ViewModels;
using HSA.Common.Models;
using HSA.Common.Utilities;
using HSA.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using HomeServiceApplication_Api.Filters;
using Microsoft.AspNetCore.Authorization;

namespace HomeServiceApplication_Api.Controllers
{
    [ServiceFilter(typeof(LoggerAttribute))]
    public class CategoryController : BaseApiController
    {
        private readonly CategoryService _service;
        private readonly ILogger<object> _logger;
        private readonly AppConfig _config;

        public CategoryController(IOptions<AppConfig> options, ILoggerFactory loggerFactory)
        {
            _config = options.Value;
            _logger = loggerFactory.CreateLogger<object>();
            _service = new CategoryService(_config, _logger);
        }

        #region GET
        [HttpGet]
        public ActionResult<ApiGridResponse<CategoryVM>> Index([FromQuery] SearchRequestModel vm)
        {
            var response = new ApiGridResponse<CategoryVM>();
            try
            {
                _logger.LogInformation($"Going to fetch Category");
                int totalCount;
                var categories = _service.GetCategories(vm, out totalCount);
                var result = new CategoryVM().FromServiceModelList(categories).ToList();

                if (categories != null && categories.Any())
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
        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<ApiResponse<int>> Post(CategoryVM vm)
        {
            ApiResponse<int> response = new();
            int code;
            string message;

            try
            {
                int categoryId = _service.CreateCategory(vm.ToServiceModel(vm), out code, out message);
                if (categoryId > 0)
                {
                    return Ok(response.GetSuccessResponseObject(categoryId, message));
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

        #region DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ApiResponse<string>> Delete(int id) 
        {
            ApiResponse<int> response = new();
            int code;
            string message;
            try
            {
                int categoryId = _service.DeleteCategory(id, out code, out message);
                if (categoryId > 0)
                {
                    return Ok(response.GetSuccessResponseObject(categoryId, message));
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
