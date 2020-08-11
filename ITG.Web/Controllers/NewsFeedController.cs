using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITG.Interfaces;
using ITG.Models.Entities;
using ITG.Models.MetaData;
using Newtonsoft.Json;
using ITG.Web.Models;
using ITG.Models.Configuration;
using System.Configuration;
using System.IO;

namespace ITG.Web.Controllers
{
    public class NewsFeedController : Controller
    {
        private IDataSource _DataSource;

        private void ConfigureDataSource(IDataSource dataSource)
        {
            // get the configuration infromation from web.config
            var fileName = ConfigurationManager.AppSettings["Articles.TestDataSourceFile"];
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"App_Data", fileName);
            var pageSize = Int32.Parse(ConfigurationManager.AppSettings["Articles.PageSize"]);

            if (string.IsNullOrEmpty(fileName) || pageSize < 1)
            {
                throw new ConfigurationErrorsException();
            }

            var config = new DataSourceConfiguration{ ConnectionSettings = path, PageSize = pageSize};

            dataSource.Configure(config);

            _DataSource = dataSource;
        }

        public NewsFeedController(IDataSource dataSource)
        {
            ConfigureDataSource(dataSource);            
        }

        //
        // GET: /NewsFeed/

        public ActionResult Index()
        {
            var startingPage = 0;

            var model = new NewsFeedViewModel
            {
                CurrentPage = startingPage,
                MetaData = _DataSource.MetaData
            };

            return View(model);
        }

        //
        // GET: /NewsFeed/Page/5

        public JsonResult Page(int id = 0)
        {
            IEnumerable<Article> output = null;
            
            output = _DataSource.GetPage(id);
            
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /NewsFeed/MetaData

        public JsonResult MetaData()
        {
            return Json(_DataSource.MetaData, JsonRequestBehavior.AllowGet);
        }
    }
}
