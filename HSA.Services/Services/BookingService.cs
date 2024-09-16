using HSA.Common.Models;
using HSA.Common.Utilities;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;
using HSA.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HSA.Services.Services
{
    public class BookingService : BaseService<BookingSM, Booking>
    {
        private readonly AppConfig _appConfig;
        private ILogger _logger;

        public BookingService(AppConfig appConfig, ILogger logger) : base(appConfig)  { 
            _appConfig = appConfig;
            _logger = logger;
        }

        public int createBooking(BookingSM sm, string UserId, out int code, out string message)
        {
            try
            {
                sm.CustomerId = int.Parse(UserId);
                Insert(sm);
                uow.SaveChanges();
                int bookingId = Get().Id;
                if (bookingId > 0)
                {
                    _logger.LogInformation($"CustomLog:BookingService: Booking Created, Booking Id: {bookingId}");
                    code = (int)HttpStatusCode.OK;
                    message = "Booking Created Successfully";
                    return bookingId;
                }
                else
                {
                    _logger.LogInformation($"CustomLog:BookingService:Failed to create Booking");
                    code = (int)HttpStatusCode.BadRequest;
                    message = "Faild to create Booking";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:BookingService: Error Occured while creating Booking. Exp: {ex}");
                message = $"Faild to create Booking {ex.Message}";
                code = (int)HttpStatusCode.BadRequest;
                return -1;
            }
        }

        public List<BookingSM> GetBookings(SearchRequestModel sm, out int totalCount) 
        {
            try
            {
                int recordsToSkip = 0;
                var query = uow.RepositoryAsync<Booking>().Queryable();

                if (sm != null && sm.pageNumber > 0 && sm.pageSize > 0)
                {
                    recordsToSkip = (sm.pageNumber - 1) * sm.pageSize;
                }
                sm.pageSize = 10;
                totalCount = query.CountAsync().Result;
                var result = query.Skip(recordsToSkip).OrderByDescending(x => x.Id)
                     .Take(sm.pageSize)
                     .ToList();
                return new BookingSM().FromDataModelList(result).ToList();
            }
            catch (Exception exp)
            {
                _logger.LogError($"CustomLog:CategoryService: Error Occured while fetching Categories. Exp: {exp}");
                throw;
            }
        }
    }
        


}
