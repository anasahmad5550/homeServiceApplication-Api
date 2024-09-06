using HSA.Common.Models;
using HSA.Common.Utilities;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;
using HSA.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Net;
using System.Security.Claims;


namespace HSA.Services.Services
{
    public class CategoryService : BaseService<CategorySM, Category>
    {
        private readonly AppConfig _appConfig;
        private ILogger _logger;
        private CompanyService _companyService;

        public CategoryService(AppConfig appConfig, ILogger logger) : base(appConfig)
        {
            _logger = logger;
            _appConfig = appConfig;
            _companyService = new CompanyService(_appConfig, _logger);
        }

        public List<CategorySM> GetCategories(SearchRequestModel sm, out int totalCount)
        {
            try
            {
                int recordsToSkip = 0;
                var query = uow.RepositoryAsync<Category>().Queryable();

                if (sm != null && !string.IsNullOrEmpty(sm.searchText))
                {
                    query = query.Where(x => x.Title.Contains(sm.searchText, StringComparison.OrdinalIgnoreCase));
                }
                if (sm != null && sm.pageNumber > 0 && sm.pageSize > 0)
                {
                    recordsToSkip = (sm.pageNumber - 1) * sm.pageSize;
                }
                sm.pageSize = 10;
                totalCount = query.CountAsync().Result;
                var result = query.Skip(recordsToSkip).OrderByDescending(x => x.Id)
                     .Take(sm.pageSize)
                     .ToList();
                return new CategorySM().FromDataModelList(result).ToList();
            }
            catch (Exception exp)
            {
                _logger.LogError($"CustomLog:CategoryService: Error Occured while fetching Categories. Exp: {exp}");
                throw;
            }
        }

        public int CreateCategory(CategorySM categorySm, out int code, out string message)
        {
            try
            {
                Insert(categorySm);
                uow.SaveChanges();
                int categoryId = Get().Id;
                if (categoryId > 0)
                {
                    _logger.LogInformation($"CustomLog:CategoryService: Category Created, Category Id: {categoryId}");
                    code = (int)HttpStatusCode.OK;
                    message = "Category Created Successfully";
                    return categoryId;
                }
                else
                {
                    _logger.LogInformation($"CustomLog:CategoryService:Failed to create Category");
                    code = (int)HttpStatusCode.BadRequest;
                    message = "Faild to create Category";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:CategoryService: Error Occured while creating Category. Exp: {ex}");
                message = $"Faild to create Category {ex.Message}";
                code = (int)HttpStatusCode.BadRequest;
                return -1;
            }


        }

        public int DeleteCategory(int Id, out int code, out string message)
        {
            try
            {
                int services = _companyService.DeleteServices(Id);
                if (services > 0) Delete(Id);
                uow.SaveChanges();
                _logger.LogInformation($"CustomLog:CategoryService: Category deleted, Category Id: {Id}");
                code = (int)HttpStatusCode.OK;
                message = "Category Deleted Successfully";
                return 1;
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:CategoryService: Error Occured while deleting Category. Exp: {ex}");
                message = $"Faild to delete Category {ex.Message}";
                code = (int)HttpStatusCode.BadRequest;
                return -1;
            }
        } 

    }
}
