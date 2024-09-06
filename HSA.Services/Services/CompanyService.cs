using HSA.Common.Models;
using HSA.Common.Utilities;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;
using HSA.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;


namespace HSA.Services.Services
{
    public class CompanyService : BaseService<ServiceSM, Service>
    {
        private readonly AppConfig _appConfig;
        private ILogger _logger;
        UserService _service;

        public CompanyService(AppConfig appConfig, ILogger logger) : base(appConfig)
        {
            _logger = logger;
            _appConfig = appConfig;
            _service = new UserService(_appConfig, _logger);
        }

        public List<ServiceSM> GetServices(SearchRequestModel sm, out int totalCount)
        {
            try
            {
                int recordsToSkip = 0;
                var query = uow.RepositoryAsync<Service>().Queryable();

                if (sm != null && !string.IsNullOrEmpty(sm.searchText))
                {
                    query = query.Where(x => x.Title.Contains(sm.searchText, StringComparison.OrdinalIgnoreCase));
                }
                if (sm.pageNumber > 0 && sm.pageSize > 0)
                {
                    recordsToSkip = (sm.pageNumber - 1) * sm.pageSize;
                }
                sm.pageSize = 10;
                totalCount = query.CountAsync().Result;
                var result = query.Skip(recordsToSkip).OrderByDescending(x => x.Id)
                     .Take(sm.pageSize)
                     .ToList();
                return new ServiceSM().FromDataModelList(result).ToList();
            }
            catch (Exception exp)
            {
                _logger.LogError($"CustomLog:CompanyService: Error Occured while fetching Companies. Exp: {exp}");
                throw;
            }
        }

        public int CreateService(ServiceSM serviceSm, out int code, out string message, string Id)
        {
            try
            {
                var user = _service.GetUser(Id);
                serviceSm.SellerId = user.Id;
                Insert(serviceSm);
                uow.SaveChanges();
                int serviceId = Get().Id;
                if (serviceId > 0)
                {
                    _logger.LogInformation($"CustomLog:CompanyService: Service Created, Service Id: {serviceId}");
                    code = (int)HttpStatusCode.OK;
                    message = "Service Created Successfully";
                    return serviceId;
                }
                else
                {
                    _logger.LogInformation($"CustomLog:CompanyService:Failed to create Service");
                    code = (int)HttpStatusCode.BadRequest;
                    message = "Faild to create Service";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:CompanyService: Error Occured while creating Service. Exp: {ex}");
                message = $"Faild to create Service {ex.Message}";
                code = (int)HttpStatusCode.BadRequest;
                return -1;
            }


        }

        public int DeleteServices(int categoryId)
        {
            try
            {
                Delete(service => service.CategoryId == categoryId);
                uow.SaveChanges();
                _logger.LogInformation("CustomLog:CompanyService: Services Deleted");
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:CompanyService: Error Occured while creating Service. Exp: {ex}");
                return -1;
            }


        }
    }
}
