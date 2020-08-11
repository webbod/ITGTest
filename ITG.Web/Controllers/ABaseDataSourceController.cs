using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITG.Interfaces;
using ITG.Models.Configuration;
using System.Configuration;


namespace ITG.Web.Controllers
{
    public abstract class ABaseDataSourceController : Controller
    {
        protected IDataSource _DataSource;

        public ABaseDataSourceController(IDataSource dataSource)
        {
            ConfigureDataSource(dataSource);            
        }

        protected void ConfigureDataSource(IDataSource dataSource)
        {
            // get the configuration infromation from web.config
            var connectionSettings = ConfigurationManager.AppSettings["DataSource.ConnectionSettings"];
            var pageSize = Int32.Parse(ConfigurationManager.AppSettings["DataSource.PageSize"]);

            if (string.IsNullOrEmpty(connectionSettings) || pageSize < 1)
            {
                throw new ConfigurationErrorsException();
            }

            var config = new DataSourceConfiguration { ConnectionSettings = connectionSettings, PageSize = pageSize };

            dataSource.Configure(config);

            _DataSource = dataSource;
        }
    }
}