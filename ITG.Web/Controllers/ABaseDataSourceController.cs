using ITG.Interfaces;
using ITG.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace ITG.Web.Controllers
{
    public abstract class ABaseDataSourceController : Controller
    {
        protected IDataSource _DataSource;

        public ABaseDataSourceController(IDataSource dataSource, IConfiguration configuration)
        {
            ConfigureDataSource(dataSource, configuration);            
        }

        protected void ConfigureDataSource(IDataSource dataSource, IConfiguration configuration)
        {
            // get the configuration infromation from web.config
            var connectionSettings = configuration.GetSection("DataSource").GetSection("ConnectionSettings").Value;
            var pageSize = Int32.Parse(configuration.GetSection("DataSource").GetSection("PageSize").Value);

            if (string.IsNullOrEmpty(connectionSettings) || pageSize < 1)
            {
                throw new ArgumentException();
            }

            dataSource.Configure(new DataSourceConfiguration { ConnectionSettings = connectionSettings, PageSize = pageSize });

            _DataSource = dataSource;
        }
    }
}