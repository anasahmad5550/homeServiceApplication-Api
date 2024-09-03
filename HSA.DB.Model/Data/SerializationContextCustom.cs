using Core.Repository.DataContext;
using HSA.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HSA.DB.Model.Data
{
    public partial class SerializationContext : DataContext
    {
        private readonly AppConfig currentTenant;
        private static string DBConnectionString { get; set; }

        public SerializationContext(AppConfig _currentTenant)
        {
            currentTenant = _currentTenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (currentTenant != null)
                {
                    optionsBuilder.UseSqlServer(currentTenant.DbConnectionString);
                    DBConnectionString = currentTenant.DbConnectionString;
                }
                else
                {
                    // if DBContext is not added through DI using AddDBContext, then get Connection string from the variable instead of hard-coded connection string
                    optionsBuilder.UseSqlServer(DBConnectionString);
                }
                //optionsBuilder.AddInterceptors(new DBIntercepter());
                base.OnConfiguring(optionsBuilder);
            }
        }
    }

}
