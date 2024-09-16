using Microsoft.AspNetCore.Mvc;
using HomeServiceApplication_Api.ViewModels;
using HomeServiceApplication_Api.Controllers.Shared;
using HSA.Common.Utilities;
using HSA.Services.Services;
using Microsoft.Extensions.Options;
using HomeServiceApplication_Api.ViewModels.Shared;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using HSA.Common.Models;

namespace HomeServiceApplication_Api.Controllers
{
    public class BookingController : BaseApiController
    {
        private readonly BookingService _service;
        private readonly ILogger<object> _logger;
        private readonly AppConfig _config;

        public BookingController(IOptions<AppConfig> options, ILoggerFactory loggerFactory)
        {
            _config = options.Value;
            _logger = loggerFactory.CreateLogger<BookingController>();
            _service = new BookingService(_config, _logger);
        }

        #region POST
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult<ApiResponse<string>> Post(CreateBookingVM vm)
        {
            ApiResponse<int> response = new();
            int code;
            string message;

            try
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int BookingId = _service.createBooking(vm.ToServiceModel(vm), UserId, out code, out message);
                if (BookingId > 0)
                {
                    return Ok(response.GetSuccessResponseObject(BookingId, message));
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

        #region GET
        [HttpGet]
        public ActionResult<ApiGridResponse<BookingVM>> Index([FromQuery] SearchRequestModel vm)
        {
            var response = new ApiGridResponse<BookingVM>();
            try
            {
                _logger.LogInformation($"Going to fetch Bookings");
                int totalCount;
                var bookings = _service.GetBookings(vm, out totalCount);
                var result = new BookingVM().FromServiceModelList(bookings).ToList( );

                if (bookings != null && bookings.Any())
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
    }
}
